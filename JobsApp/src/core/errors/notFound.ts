import { ErrorMessage } from "../models/error";

export default class NotFoundError extends Error {
  constructor(error: ErrorMessage) {
    super(error.message);
  }
}
