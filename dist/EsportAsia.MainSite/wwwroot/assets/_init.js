var ART_PARENT_URL = "/art/"

var IS_FN_MENU_OPEN = {
    home: { isOpen: false },
    esport_game: { isOpen: true },
    mytv: { isOpen: true },
    store: { isOpen: false },
    stream_show: { isOpen: true },
    favorites: { isOpen: true },
    history: { isOpen: true },
    channel: { isOpen: true },
    error_page: { isOpen: true },
}
var _TEMPURL = 'app/models/data/_temp/'

var STORE_LEFT_MENU = [
    { name: '最新商品', parentLink: '', childLink: 'all', tags: ['最新商品'] },
    { name: '精選商品', parentLink: '', childLink: 'best', tags: ['精選商品'] },
    { name: '潮流特區', parentLink: '', childLink: 'fashion', tags: ['潮流特區'] },
    { name: '品牌專區', parentLink: '', childLink: 'brand', tags: ['品牌專區'] },
    { name: '其他商品', parentLink: '', childLink: 'other', tags: ['其他商品'] },
    { name: '購物車', parentLink: '', childLink: 'cart', tags: ['購物車'], icon: _TEMPURL + 'left_icon_01.svg' },
]

var ESPORT_LEFT_MENU_GAME = [
    { name: '英雄聯盟', parentLink: '/channel/', childLink: 'lol', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    /* { name: '鬥陣特工', parentLink: '/channel/', childLink: 'overwatch', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
     { name: 'Dota2', parentLink: '/channel/', childLink: 'dota2', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },*/

]
var ESPORT_LEFT_MENU_OTHER = [
    { name: '新聞專區', parentLink: '/esport_news', childLink: '', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    { name: '戰隊資訊', parentLink: '/esport_teams', childLink: '', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
]

var ESPORT_LEFT_MENU_USER_SAVE = [
    { name: '最愛清單', parentLink: '/', childLink: 'favorites', icon: ART_PARENT_URL + 'menu/left_icon_02.svg' },
    { name: '觀看紀錄', parentLink: '/', childLink: 'history', icon: ART_PARENT_URL + 'menu/left_icon_03.svg' },
    { name: '好友', parentLink: '', childLink: '', icon: ART_PARENT_URL + 'menu/left_icon_04.svg' },
]
var SM_ESPORT_LEFT_MENU_GAME = [
    { name: '英雄聯盟', parentLink: '/channel/', childLink: 'lol', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    /* { name: '鬥陣特工', parentLink: '/channel/', childLink: 'overwatch', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
     { name: 'Dota2', parentLink: '/channel/', childLink: 'dota2', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },*/
    { name: '新聞專區', parentLink: '/esport_news', childLink: '', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    { name: '戰隊資訊', parentLink: '/esport_teams', childLink: '', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    { name: '職業專區', parentLink: '/stream_show/', childLink: 'prefessional', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    { name: '素人明星', parentLink: '/stream_show/', childLink: 'ordinary', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    //   { name: '極限樂購', parentLink: '/', childLink: 'store', icon: ART_PARENT_URL + 'menu/left_icon_01.svg' },
    { name: '最愛清單', parentLink: '/', childLink: 'favorites', icon: ART_PARENT_URL + 'menu/left_icon_02.svg' },
    { name: '觀看紀錄', parentLink: '/', childLink: 'history', icon: ART_PARENT_URL + 'menu/left_icon_03.svg' },
]
var SM_ESPORT_RIGHT_MENU_GAME = {
    name: '',
    parentLink: null,
    childLink: null,
    icon: ART_PARENT_URL + 'menu/ic_person_white_24px.svg',
    items: [
        { name: '我的頻道', parentLink: '', childLink: '' },
        //    { name: '影片管理', parentLink: '', childLink: '' },
        //    { name: '訊息通知', parentLink: '', childLink: '' },
        //    { name: 'My Donate', parentLink: '', childLink: '' },
        { name: '設定', parentLink: '', childLink: '' },
        { name: '登出', parentLink: '', childLink: '' }
    ]
}

var HEAD_MENU = [
    // { name: 'HOME', parentLink:'/', childLink:  '' },
    {
        name: '線上聯賽',
        parentLink: null,
        childLink: null,
        items: ESPORT_LEFT_MENU_GAME.concat(ESPORT_LEFT_MENU_OTHER)
            /*[
                { name: '英雄聯盟', link: '' },
                { name: '鬥陣特工', link: '' },
                { name: 'DOTA2', link: '' }
            ]*/
    },
    {
        name: '實況Show',
        parentLink: null,
        childLink: null,
        items: [
            { name: '職業專區', parentLink: '/stream_show/', childLink: 'prefessional' },
            { name: '素人明星', parentLink: '/stream_show/', childLink: 'ordinary' },
        ]
    },
    { name: '極限樂購', parentLink: null, childLink: null },
    {
        name: '我的TV',
        parentLink: '/',
        childLink: 'mytv',
        items: [
            { name: '我的頻道', parentLink: '', childLink: '' },
            //            { name: '影片管理', parentLink: '', childLink: '' },
            //            { name: '訊息通知', parentLink: '', childLink: '' },
            //            { name: 'My Donate', parentLink: '', childLink: '' },
            { name: '設定', parentLink: '', childLink: '' },
            { name: '登出', parentLink: '', childLink: '' }
        ]
    }
]

var SOCIAL_LINK = [
    { name: 'facebook', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_01.svg' },
    { name: 'youtube', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_02.svg' },
    { name: 'twitch', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_03.svg' },
    { name: 'line', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_04.svg' },
    { name: 'Instagram', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_05.svg' },
    { name: 'weibo', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_06.svg' },
    { name: 'wechat', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_07.svg' },
    { name: 'meipai', parentLink: null, childLink: '', icon: ART_PARENT_URL + 'social/community_icon_08.svg' },
]