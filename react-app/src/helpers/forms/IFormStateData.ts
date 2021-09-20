import { FormErrors } from "./FormErrors";

export interface IFormStateData{
    [key: string]: any;
    errors: FormErrors
}