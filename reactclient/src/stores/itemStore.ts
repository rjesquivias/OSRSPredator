import { makeAutoObservable } from "mobx";

export default class ItemStore {
        simpleItemAnalysisList: any[] = [];
        pageSize: number = 20
        navState: string = "All Items"
        checkedItems: any[] = [];
        namePressed: boolean = false;

        constructor() {
            makeAutoObservable(this)
        }

        setSimpleItemAnalysisList = (itemAnalysisList: any[]) => {
            this.simpleItemAnalysisList = itemAnalysisList;
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
}