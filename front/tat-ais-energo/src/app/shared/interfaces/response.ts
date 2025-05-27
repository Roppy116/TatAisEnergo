export interface IResponse<T> {
  data?: (T);
  errors: Error[];
}

export class Error {
  code?: number;
  message?: string;
}
