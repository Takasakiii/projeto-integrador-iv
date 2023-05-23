import { observable } from "@legendapp/state";
import { User } from "./models/user";

export const userStorage = observable<User | null>(null);
