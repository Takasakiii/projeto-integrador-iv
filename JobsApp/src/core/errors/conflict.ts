import { ErrorMessage } from "../models/error";

export default class ConflictError extends Error {
  constructor(error: ErrorMessage) {
    super(error.message);
  }
}
