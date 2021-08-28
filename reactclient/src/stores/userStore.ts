import axios from "axios";
import { makeAutoObservable, runInAction } from "mobx";
import { history } from "..";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

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
            const response = await axios.post("https://localhost:5001/api/Account/login", creds);
            const user = response.data;
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/itemDashboard');
            store.modalStore.closeModal();
        } catch(error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;
        history.push('/');
    }

    getUser = async () => {
        try {
            const response = await axios.get("https://localhost:5001/api/Account");
            const user = response.data;
            runInAction(() => this.user = user);
        } catch(error) {
            console.log(error);
        }
    }

    register = async (creds: UserFormValues) => {
        try{
            const response = await axios.post("https://localhost:5001/api/Account/register", creds);
            const user = response.data;
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/itemDashboard');
            store.modalStore.closeModal();
        } catch(error) {
            throw error;
        }
    }
}