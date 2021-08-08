import axios from "axios";
import { makeAutoObservable } from "mobx";

export default class ItemStore {
        simpleItemAnalysisList: any[] = [];
        simpleItemAnalysisMap: Map<number, any> = new Map();
        pageSize: number = 20;
        navState: string = "All Items";
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
            var response = await axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${this.pageSize}&page=1`);
            this.setSimpleItemAnalysisList(response.data);
        }

        updatePage = async (page: number | string | undefined) => {
            if(this.navState === "All Items") {
                var response = await axios.get(`https://localhost:5001/api/v1/Analytics?pageSize=${this.pageSize}&page=${page}`);
                this.setSimpleItemAnalysisList(response.data);
            } else {
                // TODO: Implement frontend pagination
            }
        }
}