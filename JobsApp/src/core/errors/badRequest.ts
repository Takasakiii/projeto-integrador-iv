import { ErrorMessage } from "../models/error";

export default class BadRequestError extends Error {
  constructor(error: ErrorMessage) {
    super(error.message);
  }
}
