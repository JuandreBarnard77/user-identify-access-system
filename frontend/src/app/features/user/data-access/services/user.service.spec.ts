import { TestBed } from '@angular/core/testing';
import { UserComponent } from '../../pages/user.component';
import { UserService } from './user.service';
import { MockUserService } from './user.service.mock';

describe('UserComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [UserComponent],
      providers: [{ provide: UserService, useClass: MockUserService }],
    });
  });

  it('should load mock users', () => {
    const fixture = TestBed.createComponent(UserComponent);
    const component = fixture.componentInstance;
  });
});

