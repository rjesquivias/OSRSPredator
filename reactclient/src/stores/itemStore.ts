import axios from "axios";
import { makeAutoObservable } from "mobx";

export default class ItemStore {
    readonly ALL_ITEMS = "All Items";
    readonly WATCHLIST = "Watchlist";
    readonly WATCH_ITEMS = "Watch Items";
    readonly UNWATCH_ITEMS = "Unwatch Items";
    readonly ITEM_AVATAR_URL = "https://services.runescape.com/m=itemdb_oldschool/obj_big.gif?id=";
    readonly ITEM_DETAILS_URL = "https://localhost:5001/api/v1/ItemDetails";
    readonly ITEM_WATCHLIST_URL = "https://localhost:5001/api/v1/WatchList";
    readonly QUERY_PARAM_PAGESIZE = "pageSize";
    readonly QUERY_PARAM_PAGE = "page";

    simpleItemAnalysisList: any[] = [];
    simpleItemAnalysisMap: Map<number, any> = new Map();
    simpleItemAnalysisImageLoadedMap: Map<number, boolean> = new Map();
    pageSize: number = 20;
    navState: string = this.ALL_ITEMS;
    checkedItems: any[] = [];
    namePressed: boolean = false;
    page: number = 1;
    totalPages = 3800/this.pageSize;
    isListLoading: boolean = true;
    isDetailsLoading: boolean = true;
    selectedDetailsItem: any;

    constructor() {
        makeAutoObservable(this)
    }

    setSimpleItemAnalysisList = (itemAnalysisList: any[]) => {
        this.simpleItemAnalysisList = itemAnalysisList;
        this.simpleItemAnalysisList.map((item: any) => {
            this.simpleItemAnalysisMap.set(item.id, item)
        });
    }

    setNavState = (navState: string) => {
        this.navState = navState;
    }

    getNavState = (): string => {
        return this.navState
    }

    setCheckedItems = (checkedItems: any[]) => {
        this.checkedItems = checkedItems;
    }

    setNamePressed = (namePressed: boolean) => {
        this.namePressed = namePressed;
    }

    setIsListLoading = (isListLoading: boolean) => {
        this.isListLoading = isListLoading;
    }

    getIsListLoading = (): boolean => {
        return this.isListLoading;
    }

    setIsDetailsLoading = (isDetailsLoading: boolean) => {
        this.isDetailsLoading = isDetailsLoading;
    }

    getIsDetailsLoading = (): boolean => {
        return this.isDetailsLoading;
    }

    setImageLoaded = (id: number, loaded: boolean) => {
        this.simpleItemAnalysisImageLoadedMap.set(id, loaded);
    }

    getSelectedDetailsItem = (): any => {
        return this.selectedDetailsItem;
    }

    isImageLoaded = (id: number) => {
        var result = this.simpleItemAnalysisImageLoadedMap.get(id);
        
        if(result == undefined) return false;
        return result;
    }

    loadAllItems = async (page: number | string | undefined) => {
        this.setIsListLoading(true);
        var response = await axios.get(`${this.ITEM_DETAILS_URL}?${this.QUERY_PARAM_PAGESIZE}=${this.pageSize}&${this.QUERY_PARAM_PAGE}=${page}`);
        console.log(response.data);
        this.setSimpleItemAnalysisList(response.data);
        this.setIsListLoading(false);
    }

    getItemAnalytics = async (id: string) => {
        this.isDetailsLoading = true
        this.selectedDetailsItem = await axios.get(`${this.ITEM_DETAILS_URL}/${id}`)
        this.isDetailsLoading = false
    }

    loadWatchList = async () => {
        this.setIsListLoading(true);
        var response = await axios.get(`${this.ITEM_WATCHLIST_URL}?${this.QUERY_PARAM_PAGESIZE}=${this.pageSize}&${this.QUERY_PARAM_PAGE}=1`);
        console.log(response.data);
        this.setSimpleItemAnalysisList(response.data);
        this.setIsListLoading(false);
    }

    watchItems = async () => {
        for (const item of this.checkedItems)
        {
            await axios.post(this.ITEM_WATCHLIST_URL, item);
        }
    }

    unwatchItems = async () => {
        for (const item of this.checkedItems)
        {
            await axios.delete(this.ITEM_WATCHLIST_URL, { data: item });
        }

        this.setCheckedItems([]);
        this.loadWatchList();
    }
}