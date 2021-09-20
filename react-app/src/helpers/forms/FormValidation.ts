import { Dictionary } from "../Dictionary";
import { IFormStateData } from "./IFormStateData";

const emailRegex = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

interface FormValidationTestValue{
    validationKey: string;
    testValue?: any;
    alternativeValidationKey?: string
}

interface FormFieldToValidate{
    fieldName: string;
    validations: Array<FormValidationTestValue>;
}

interface FormValidator {
    needApiCall: boolean
    execute: (stateData: IFormStateData, fieldName: string, testValue: any, alternativeValidationKey?: string) => Promise<boolean>
}

export class FormValidation{    
    private formFields: FormFieldToValidate[];
    private valid: boolean = false;

    constructor(formFields: FormFieldToValidate[]){
        this.formFields = formFields;
    }

    private validators: Dictionary<FormValidator> = {
        "required": {
            needApiCall: false,
            execute: (stateData: IFormStateData, fieldName: string, testValue: any, alternativeValidationKey?: string) => {
                const result = stateData[fieldName] ? false : true
                stateData.errors[fieldName][alternativeValidationKey || "required"] = result;
                return Promise.resolve(result);
            }
        },
        "minLength": {
            needApiCall: false,
            execute: (stateData: IFormStateData, fieldName: string, min: number, alternativeValidationKey?: string) => {
                const result = (stateData[fieldName] as string).length < min;
                stateData.errors[fieldName][alternativeValidationKey || "minLength"] = result;
                return Promise.resolve(result);
            }
        },
        "maxLength": {
            needApiCall: false,
            execute: (stateData: IFormStateData, fieldName: string, max: number, alternativeValidationKey?: string) => {
                const result = (stateData[fieldName] as string).length > max;
                stateData.errors[fieldName][alternativeValidationKey || "maxLength"] = result;
                return Promise.resolve(result);
            }
        },
        "pattern": {
            needApiCall: false,
            execute: (stateData: IFormStateData, fieldName: string, pattern: RegExp, alternativeValidationKey?: string) => {
                const result = !pattern.test(stateData[fieldName]);
                stateData.errors[fieldName][alternativeValidationKey || "pattern"] = result
                return Promise.resolve(result);
            }
        },
        "email": {
            needApiCall: false,
            execute: (stateData: IFormStateData, fieldName: string, testValue: any, alternativeValidationKey?: string) => {            
                const result = !emailRegex.test(stateData[fieldName]);
                stateData.errors[fieldName][alternativeValidationKey || "email"] = result;
                return Promise.resolve(result);
            }
        }
    };

    public validate(stateData: IFormStateData, doApiCall?: boolean, validationKeyToReset?: string) : Promise<void>{
        return new Promise((resolve, reject) => {
            const promises: Promise<boolean>[] = [];            
            this.formFields.forEach((f) => {
                f.validations.forEach((v) => {
                    if(this.validators[v.validationKey]){
                        if(!this.validators[v.validationKey].needApiCall){
                            promises.push(this.validators[v.validationKey].execute(stateData, f.fieldName, v.testValue, v.alternativeValidationKey));
                        } else {
                            if(doApiCall) {
                                promises.push(this.validators[v.validationKey].execute(stateData, f.fieldName, v.testValue, v.alternativeValidationKey));
                            } else {
                                if(validationKeyToReset === v.validationKey){
                                    stateData.errors[f.fieldName][v.validationKey] = false;
                                }
                                promises.push(Promise.resolve(stateData.errors[f.fieldName][v.validationKey]));
                            }
                        }
                    }
                });
            });

            Promise.all(promises).then((values) => {
                // Valid - IF NOT (some validator has error)
                this.valid = !(values.some(hasError => hasError));
                resolve();
            });
        });                     
    }

    public addCustomValidator(validationKey: string, customValidator: (stateData: IFormStateData, fieldName: string, testValue: any) => Promise<boolean>, needApiCall: boolean = false){
        this.validators[validationKey] = { needApiCall: needApiCall, execute: customValidator };
    }

    isValid(): boolean{
        return this.valid;
    }
}