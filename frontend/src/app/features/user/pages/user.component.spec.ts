import { TestBed } from '@angular/core/testing';
import { UserComponent } from './user.component';
import { UserService } from '../data-access/services/user.service';
import { MockUserService } from '../data-access/services/user.service.mock';

describe('UserComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [UserComponent], // Standalone component
      providers: [{ provide: UserService, useClass: MockUserService }], // ✅ Inject mock
    });
  });

  it('should load mock users', () => {
    const fixture = TestBed.createComponent(UserComponent);
    const component = fixture.componentInstance;

  });
});
