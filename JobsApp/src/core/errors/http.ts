import { AxiosError } from "axios";

export default class HttpError extends Error {
  constructor(public error: AxiosError) {
    super(error.message);
  }
}
