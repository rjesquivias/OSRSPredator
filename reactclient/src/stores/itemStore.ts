import axios from "axios";
import { makeAutoObservable } from "mobx";

export default class ItemStore {
    readonly ALL_ITEMS = "All Items";
    readonly WATCHLIST = "Watchlist";
    readonly WATCH_ITEMS = "Watch Items";
    readonly UNWATCH_ITEMS = "Unwatch Items";
    readonly ITEM_AVATAR_URL = "https://services.runescape.com/m=itemdb_oldschool/obj_big.gif?id=";
    readonly ITEM_ANALYTICS_URL = "https://localhost:5001/api/v1/Analytics";
    readonly ITEM_PRICE_SNAPSHOT_URL = "https://localhost:5001/api/ItemPriceSnapshot";
    readonly ITEM_DETAIL_URL = "https://localhost:5001/api/ItemDetail";
    readonly ITEM_WATCHLIST_URL_PATH = "/Watchlist";
    readonly ITEM_UNWATCHLIST_URL_PATH = "/Unwatchlist";
    readonly QUERY_PARAM_PAGESIZE = "pageSize";
    readonly QUERY_PARAM_PAGE = "page";

    simpleItemAnalysisList: any[] = [];
    simpleItemAnalysisMap: Map<number, any> = new Map();
    pageSize: number = 20;
    navState: string = this.ALL_ITEMS;
    checkedItems: any[] = [];
    namePressed: boolean = false;
    page: number = 1;
    totalPages = 3800/this.pageSize;

    constructor() {
        makeAutoObservable(this)
    }

    setSimpleItemAnalysisList = (itemAnalysisList: any[]) => {
        this.simpleItemAnalysisList = itemAnalysisList;
        this.simpleItemAnalysisList.map((item: any) => this.simpleItemAnalysisMap.set(item.itemDetails.id, item));
    }

    setNavState = (navState: string) => {
        this.navState = navState;
    }

    setCheckedItems = (checkedItems: any[]) => {
        this.checkedItems = checkedItems;
    }

    setNamePressed = (namePressed: boolean) => {
        this.namePressed = namePressed;
    }

    loadSimpleItemAnalysisList = async () => {
        var response = await axios.get(`${this.ITEM_ANALYTICS_URL}?${this.QUERY_PARAM_PAGESIZE}=${this.pageSize}&${this.QUERY_PARAM_PAGE}=1`);
        this.setSimpleItemAnalysisList(response.data);
    }

    loadWatchList = async () => {
        var response = await axios.get(`${this.ITEM_ANALYTICS_URL}${this.ITEM_WATCHLIST_URL_PATH}?${this.QUERY_PARAM_PAGESIZE}=${this.pageSize}&${this.QUERY_PARAM_PAGE}=1`);
        var watchlist = response.data;
        const promises: any[] = [];

        watchlist.forEach((item: any) => {
            promises.push(this.getItemDetails(item))
        })

        await Promise.all(promises);
        this.setSimpleItemAnalysisList(watchlist)
    }

    getItemDetails = async (item: any) => {
        var responses = await axios.all([
            axios.get(`${this.ITEM_PRICE_SNAPSHOT_URL}/${item.snapshotId}`),
            axios.get(`${this.ITEM_DETAIL_URL}/${item.detailsId}`)
        ]);
      
        item.mostRecentSnapshot = responses[0].data;
        item.itemDetails = responses[1].data;
        item.id = 0;
    }

    updatePage = async (page: number | string | undefined) => {
        if(this.navState === this.ALL_ITEMS) {
            var response = await axios.get(`${this.ITEM_ANALYTICS_URL}?${this.QUERY_PARAM_PAGESIZE}=${this.pageSize}&${this.QUERY_PARAM_PAGE}=${page}`);
            this.setSimpleItemAnalysisList(response.data);
        } else {
            // TODO: Implement frontend pagination
        }
    }

    watchItems = async () => {
        // convert list of names into the proper post format
        for (const id of this.checkedItems)
        {
            var response = await axios.get(`${this.ITEM_ANALYTICS_URL}/${id}`);
            axios.post(this.ITEM_ANALYTICS_URL + this.ITEM_WATCHLIST_URL_PATH, response.data);
        }
    }

    unwatchItems = async () => {
        // convert list of names into the proper post format
        for (const id of this.checkedItems)
        {
            var response = await axios.get(`${this.ITEM_ANALYTICS_URL}/${id}`);
            await axios.post(this.ITEM_ANALYTICS_URL + this.ITEM_UNWATCHLIST_URL_PATH, response.data);
        }

        this.loadWatchList();
    }
}