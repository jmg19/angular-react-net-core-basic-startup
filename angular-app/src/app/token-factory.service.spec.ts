import { TestBed } from '@angular/core/testing';

import { TokenFactoryService } from './token-factory.service';

describe('TokenServiceService', () => {
  let service: TokenFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
