using FluentValidation.Results;
using MediatR;
using RC.Core.Data;
using RC.Core.Messages;
using RC.Customer.API.Application.Events;
using RC.Customer.API.Data.Repositories;
using RC.Customer.API.Domain;
using RC.MessageBus.Mediator;

namespace RC.Customer.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<AddCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MediatREventList _eventList;
        private ILogger<CustomerCommandHandler> _logger;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, MediatREventList eventList, ILogger<CustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _eventList = eventList;
            _logger = logger;
        }

        public async Task<ValidationResult> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AddCustomerCommand called");

            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = new BusinessCustomer(request.Name, request.Email, request.NationalId);

            var storedCustomer = _customerRepository.GetByNationalId(customer.NationalId?.Number);

            if (storedCustomer != null)
            {
                AddError("Customer already exists");
                return ValidationResult;
            }

            _unitOfWork.BeginTransaction();

            try
            {
                customer = _customerRepository.Add(customer);

                _eventList.AddEvent(new CustomerAddedEvent(customer.Id, customer.Name, customer.Email?.EmailAddress, customer.NationalId?.Number));

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                AddError("An error ocurred while creating the customer");
                return ValidationResult;
            }

            return ValidationResult;
        }
    }
}
