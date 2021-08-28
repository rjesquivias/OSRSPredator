import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ItemStore from "./itemStore";
import UserStore from "./userStore";

interface Store {
    itemStore: ItemStore
    commonStore: CommonStore
    userStore: UserStore
}

export const store: Store = {
    itemStore: new ItemStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}