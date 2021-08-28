import axios from "axios";
import { makeAutoObservable } from "mobx";
import { User, UserFormValues } from "../models/user";

export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async(creds: UserFormValues) => {
        try{
            const user = await axios.post("https://localhost:5001/api/Account/login", creds);
            console.log(user);
        } catch(error) {
            throw error;
        }
    }
}