import { Role } from "./role";

export interface User {
  id: string;
  userName: string;
  password: string;
  rememberMe?: boolean;
  token?: string;
  roles?: Role[];
  //For different id checking
  blockGuard?: boolean;
}
