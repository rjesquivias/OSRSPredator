import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ItemStore from "./itemStore";

interface Store {
    itemStore: ItemStore
    commonStore: CommonStore
}

export const store: Store = {
    itemStore: new ItemStore(),
    commonStore: new CommonStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}