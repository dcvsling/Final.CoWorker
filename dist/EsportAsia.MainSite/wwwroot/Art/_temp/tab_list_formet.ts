export interface iTabListFormat {
    title: string,
        css: string,
        tab_page: iTabPage[]
}
export interface iTabPage {
    tab_title: string,
        css: string,
        pre_page_number: iPrePageNumber,
        child_component: string
}

export interface iPrePageNumber {
    sm: number,
        md: number,
        xm: number
}

export let MYFAVORITE: iTabListFormat = {
    title: "最愛清單",
    css: "",
    tab_page: [{
        tab_title: "影片",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }, {
        tab_title: "頻道",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }]
}

export let HISTORY: iTabListFormat = {
    title: "最愛清單",
    css: "",
    tab_page: [{
        tab_title: "影片",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }, {
        tab_title: "頻道",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }]
}

export let CHNNEL: iTabListFormat = {
    title: null,
    css: "",
    tab_page: [{
        tab_title: "About Me",
        css: "",
        pre_page_number: { sm: 1, md: 1, xm: 1 },
        child_component: ""
    }, {
        tab_title: "影片",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }, {
        tab_title: "粉絲",
        css: "",
        pre_page_number: { sm: 10, md: 20, xm: 30 },
        child_component: ""
    }]
}
