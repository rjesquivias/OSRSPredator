import { createContext, useContext } from "react";
import ItemStore from "./itemStore";

interface Store {
    itemStore : ItemStore
}

export const store: Store = {
    itemStore: new ItemStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}