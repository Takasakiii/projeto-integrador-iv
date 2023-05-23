import { ErrorMessage } from "../models/error";

export default class UnauthorizedError extends Error {
  constructor(error: ErrorMessage) {
    super(error.message);
  }
}
