import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { GlobalService } from '../global.service';
import { environment } from './../../environments/environment';
import { Observable, of } from 'rxjs';
import { MessageService, LogMessageType } from '../message.service';
import { PopupsService } from '../popups.service';

class HttpOptions{
  public headers: HttpHeaders;

  constructor(private global: GlobalService){
    this.headers = new HttpHeaders()
      .append("UUID", this.global._UUID() )
      .append("Token", this.global._Token() );
  }
}

export class BaseHttpClient {
  constructor(
    private http: HttpClient,
    private global: GlobalService,
    private messageService: MessageService,
    private popups: PopupsService
  ){}

  private baseTap<T>(type: string, apiServicePath: string){
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
    return this.http.get<T>(`${environment.baseApiUrl}${apiServicePath}`, httpOptions).pipe(this.baseTap("GET", apiServicePath), this.baseCatchError("GET", apiServicePath));
  }

  protected post<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return this.http.post<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions).pipe(this.baseTap("POST", apiServicePath), this.baseCatchError("POST", apiServicePath));
  }

  protected put<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return this.http.put<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions).pipe(this.baseTap("PUT", apiServicePath), this.baseCatchError("PUT", apiServicePath));
  }

  protected patch<T>(apiServicePath: string, data: any) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return this.http.patch<T>(`${environment.baseApiUrl}${apiServicePath}`, data, httpOptions).pipe(this.baseTap("PATCH", apiServicePath), this.baseCatchError("PATCH", apiServicePath));
  }

  protected delete<T>(apiServicePath: string) : Observable<any> {
    const httpOptions: HttpOptions = new HttpOptions(this.global);
    this.popups.incrementPendingProcess();
    return this.http.delete<T>(`${environment.baseApiUrl}${apiServicePath}`, httpOptions).pipe(this.baseTap("DELETE", apiServicePath), this.baseCatchError("DELETE", apiServicePath));
  }
}
