import { Injectable } from '@angular/core';
import { IdentityService } from '../services/identity.service';
import { handlerExternUserError } from './identity-error-handler';

@Injectable({
  providedIn: 'root'
})
export class IdentityErrorMiddlewareService {

  constructor(private _identityService: IdentityService) { }

  handleIdentityError = (resetLocalStorage = false) => handlerExternUserError(this._identityService, resetLocalStorage);
}
