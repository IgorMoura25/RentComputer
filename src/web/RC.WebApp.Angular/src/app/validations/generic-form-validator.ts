import { FormGroup } from '@angular/forms';

export interface DisplayMessage {
    [key: string]: string
}

export interface ValidationMessages {
    [key: string]: { [key: string]: string }
}

export class GenericFormValidator {

    constructor(private validationMessages: ValidationMessages) { }

    processMessages(container: FormGroup): { [key: string]: string } {
        let messages = {};

        // Para cada item da propriedade controls do FormGroup
        // o algoritmo irá descobrir se existe mensagem a ser processada
        // e se existir irá atribuir cada mensagem ao seu item respectivo
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];

                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = '';

                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[controlKey][messageKey] + '<br />';
                                }
                            });
                        }
                    }
                }
            }
        }

        return messages;
    }
}