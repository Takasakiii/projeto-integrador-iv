import { ErrorMessage } from "../models/error";

export default class ForbiddenError extends Error {
  constructor(error: ErrorMessage) {
    super(error.message);
  }
}
