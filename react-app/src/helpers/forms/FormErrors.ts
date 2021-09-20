import { Dictionary } from "../Dictionary";

export interface FormErrors{
  [key: string]: Dictionary<boolean>;
}
