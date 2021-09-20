import { catchError, map, tap } from 'rxjs/operators';
import { IGlobalService } from '../GlobalService';
import { environment } from '../environments/environment';
import { Observable, of, from } from 'rxjs';
import { IMessageService, LogMessageType } from '../MessageService';
import { IPopupsService } from '../PopupsService';
import http, { AxiosRequestConfig } from "axios";


class HttpOptions implements AxiosRequestConfig{
  public headers: any = {};

  constructor(private global: IGlobalService){
    this.headers["UUID"] = this.global._UUID();
    this.headers["Token"] = this.global._Token();
  }
}

export class BaseHttpClient {
  constructor(    
    private global: IGlobalService,
    private messageService: IMessageService,
    private popups: IPopupsService
  ){}

  private baseTap(type: string, apiServicePath: string){
    return tap((result) => {
      const className: string = this.constructor.name;
      this.popups.decrementPendingProcess();
      this.log(`${className} on a ${type} to ${apiServicePath} succeed.`, LogMessageType.INFO, result);
    });
  }

  private baseCatchError<T>(type: string, apiServicePath: string){
    const className: string = this.constructor.name;
    this.popups.decrementPendingProcess();
    return catchError(this.handleError<T>(`${className} on a ${type} to ${apiServicePath}`));
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      if(error.status === 200){
        result = error.error.text;
        return of(result as T);
      }

      console.error(error);

      this.log(`${operation} failed:\n ${error.message}`, LogMessageType.ERROR, error);

      return of(result as T);
    };
  }

  private log(message: string, type: LogMessageType, details: any) {
    this.messageService.add(message, type, details);
  }

  protected get<T>(apiServicePath: string) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return from(http.get<T>(`${environment.baseApiUrl}${apiServicePath}`, httpOptions)).pipe(map(x => x.data), this.baseTap("GET", apiServicePath), this.baseCatchError("GET", apiServicePath));
  }

  protected post<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return from(http.post<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions)).pipe(map(x => x.data), this.baseTap("POST", apiServicePath), this.baseCatchError("POST", apiServicePath));
  }

  protected put<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return from(http.put<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions)).pipe(map(x => x.data), this.baseTap("PUT", apiServicePath), this.baseCatchError("PUT", apiServicePath));
  }

  protected patch<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return from(http.patch<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions)).pipe(map(x => x.data), this.baseTap("PATCH", apiServicePath), this.baseCatchError("PATCH", apiServicePath));
  }

  protected delete<T>(apiServicePath: string) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return from(http.delete<T>(`${environment.baseApiUrl}${apiServicePath}`, httpOptions)).pipe(map(x => x.data), this.baseTap("DELETE", apiServicePath), this.baseCatchError("DELETE", apiServicePath));
  }
}
