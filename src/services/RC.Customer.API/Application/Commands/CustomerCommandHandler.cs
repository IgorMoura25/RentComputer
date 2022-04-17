﻿using FluentValidation.Results;
using MediatR;
using RC.Core.Data;
using RC.Core.Mediator;
using RC.Core.Messages;
using RC.Customer.API.Application.Events;
using RC.Customer.API.Data.Repositories;
using RC.Customer.API.Domain;

namespace RC.Customer.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<AddCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MediatREventList _eventList;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, MediatREventList eventList)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _eventList = eventList;
        }

        public async Task<ValidationResult> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
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

            await _unitOfWork.BeginTransaction();

            try
            {
                customer = _customerRepository.Add(customer);

                _eventList.AddEvent(new CustomerAddedEvent(customer.Id, customer.Name, customer.Email?.EmailAddress, customer.NationalId?.Number));

                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
                AddError("An error ocurred while creating the customer");
                return ValidationResult;
            }

            return ValidationResult;
        }
    }
}
