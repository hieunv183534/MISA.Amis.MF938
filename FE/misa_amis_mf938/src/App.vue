<template>
    <div id="app">
        <div :class="['menu',{'menu-collapse': isCollapse}]">
            <div class="menu-header">
                <div v-if="isCollapse" class="toggle-menu background-icon position-icon-menu-toggle-white" @click="unCollapseMenu"></div>
                <div v-if="!isCollapse" class="menu-option background-icon position-icon-menu-option"></div>
                <div v-if="!isCollapse" class="logo"></div>
            </div>
            <div class="menu-list-scroller">
                <div class="menu-list">
                    <MenuItem v-for="(menuItem, index) in menuList"
                              :key="index"
                              :menuItemText="menuItem.menuItemText"
                              :menuItemClass="menuItem.menuItemClass"
                              :title="menuItem.menuItemText" 
                              :isCollapse="isCollapse"/>
                </div>
            </div>
        </div>
        <div :class="['content',{'content-collapse' : isCollapse}]">
            <div class="content-header">
                <div class="content-header-left">
                    <div v-if="!isCollapse" class="toggle-menu background-icon position-icon-menu-toggle" @click="collapseMenu"></div>
                    <div class="company">CÔNG TY CỔ PHẦN HIEUNV</div>
                    <div class="company-option background-icon position-icon-chevron-right"></div>
                </div>
                <div class="content-header-right">
                    <div class="notification">
                        <div class="notification-icon background-icon position-icon-ring"></div>
                        <div class="triangle-wrapper">
                            <div class="triangle"></div>
                        </div>
                    </div>
                    <div class="user">
                        <div class="avatar background-icon position-icon-avatar-default"></div>
                        <p class="display-user">Nguyễn Văn Hiếu</p>
                        <div class="user-option background-icon position-icon-chevron-down"></div>
                    </div>
                </div>
            </div>
            <EmployeeList />
        </div>
        <Popup :popupListButton="popupListButton"
               :popupType="popupType"
               :popupText="popupText"
               v-if="isShowPopup" 
               @popupOnConfirm="popupOnConfirm"/>
        <Loader :isShowLoader="isShowLoader"/>
        <ToastMessenger :mesType="mesType" :mesText="mesText" :isShowMes="isShowMes" @hideMes="hideMes"/>
    </div>
</template>

<script>
    import {PopupEventBus, EventBus, LoaderEventBus,ToastMessengerEventBus} from "./main.js"
    import MenuItem from "./components/layout/MenuItem.vue"
    import Popup from "./components/base/Popup.vue"
    import ToastMessenger from "./components/base/ToastMessenger.vue"
    import Loader from "./components/base/Loader.vue"
    import { menuList } from "./models/menuList"
    import EmployeeList from "./view/employee/EmployeeList.vue"

    export default {
        name: 'App',
        components: {
            MenuItem, EmployeeList, Popup, Loader, ToastMessenger
        },
        data() {
            return {
                // biến kiểm soát sự kiện collapse menu
                isCollapse: false,
                // giá trị ẩn hoặc hiện toastMessenger
                isShowMes: false,
                mesText: '',
                mesType: '',
                // list các choose của menu
                menuList: menuList,
                // các biến truyền vào cho popup
                popupListButton: "t",
                popupType: 'danger',
                isShowPopup: false,
                isShowLoader: false,
                popupText: 'Aliquet harum inventore. Porttitor? Ultrices nulla, laudantium nostrud, ',
                modeForPopup: ''
                //---------------------------------
            }
        },
        created() {

            // Lắng nghê sự kiện show Popup confirm
            PopupEventBus.$on('showPopup', (_modeForPopup, _popupText, _popupType, _popupListButton) => {
                this.isShowPopup = true;
                this.modeForPopup = _modeForPopup;
                this.popupText = _popupText;
                this.popupType = _popupType;
                this.popupListButton = _popupListButton;
            });

            // Lắng nghe sự kiện hiện loader
            LoaderEventBus.$on('showLoader', () => {
                this.isShowLoader = true;
            })

            // lắng nghe sự kiện ẩn loader
            LoaderEventBus.$on('hideLoader', () => {
                this.isShowLoader = false;
            })

            //Lắng nghe sự kiện hiện toastMessenger
            ToastMessengerEventBus.$on('showMes', (_mesText, _mesType) => {
                this.mesText = _mesText;
                this.mesType = _mesType;
                this.isShowMes = true;
            })
        },
        methods: {
            /**
             * Xử lí sự kiện click vào 1 btn nào đó trên popup, dựa vào modeForPopup với btnClicked để xử lí sự kiện tương ứng
             * @param btnClicked
             * Author HieuNV
             */
            popupOnConfirm(btnClicked) {
                switch (btnClicked) {
                    case 'x':
                        if (this.modeForPopup == 'delete') {
                            EventBus.$emit('confirmToDelete');
                            this.isShowPopup = false;
                        } else if (this.modeForPopup == 'save') {
                            EventBus.$emit('confirmToSave');
                            this.isShowPopup = false;
                        } else if (this.modeForPopup == 'saveAndAdd') {
                            EventBus.$emit('confirmToSaveAndAdd');
                            this.isShowPopup = false;
                        }
                        break;
                    case 'y':
                        if (this.modeForPopup == 'save' || this.modeForPopup == 'saveAndAdd') {
                            this.isShowPopup = false;
                            EventBus.$emit('hideDialog');
                        }
                        break;
                    case 'z':
                        if (this.modeForPopup == 'save' || this.modeForPopup == 'saveAndAdd') {
                            this.isShowPopup = false;
                        }
                        break;
                    case 't':
                        if (this.modeForPopup == 'messenger') {
                            this.isShowPopup = false;
                        }
                        break;
                    case 'w':
                        if (this.modeForPopup == 'delete') {
                            this.isShowPopup = false;
                        }
                        break;
                    case 'u':
                        if (this.modeForPopup == 'validate') {
                            alert("Đồng ý")
                        }
                        break;
                    default:
                        break;
                }
            },
            /**
             * ẩn ToastMessenger
             * */
            hideMes() {
                this.isShowMes = false;
            },
            /**
             * Thu nhỏ menu
             * */
            collapseMenu() {
                this.isCollapse = true;
            },
            /**
             * Trả lại kích thước menu
             * */
            unCollapseMenu() {
                this.isCollapse = false;
            }
        }
    }
</script>

<style>
    @import url('css/icon-position.css');

    @font-face {
        font-family: 'NotosansBold';
        src: url('assets/fonts/notosans-bold.3ede5aff.woff2') format('woff2');
    }

    @font-face {
        font-family: 'NotosansItalic';
        src: url('assets/fonts/notosans-italic.1c1aaac9.woff2') format('woff2');
    }

    @font-face {
        font-family: 'NotosansRegular';
        src: url('assets/fonts/notosans-regular.2cb88a13.woff2') format('woff2');
    }

    @font-face {
        font-family: 'NotosansSemibold';
        src: url('assets/fonts/notosans-semibold.df3e6dc4.woff2') format('woff2');
    }

    ::-webkit-scrollbar {
  width: 10px;
  background-color: #F1F1F1;
}

::-webkit-scrollbar-thumb {
  background-color: #B8BCC3;
}

::-webkit-scrollbar-thumb:hover {
  background-color: #888888;
}

::-webkit-scrollbar:horizontal {
  height: 10px;
  background-color: #F1F1F1;
}

::-webkit-scrollbar-thumb:horizontal {
  background: #B8BCC3;
}

::-webkit-scrollbar-thumb:horizontal:hover {
  background: #888888;
}

    body {
        padding: 0;
        margin: 0;
        font-family: NotosansRegular;
        color:#111111;
        font-size: 13px;
    }

    .toggle-menu{
        cursor:pointer;
    }

    .menu {
        float: left;
        width: 178px;
        height: 100vh;
        background-color: #393A3D;
    }

    .menu-collapse{
        width: 52px;
    }

        .menu-collapse .menu-header{
            justify-content: center !important;
        }

        .menu-header {
            width: 100%;
            height: 48px;
            display: flex;
            align-items: center;
            justify-content: flex-start;
            padding: auto 24px auto 24px;
            box-sizing: border-box;
        }

        .menu-header .menu-option {
            margin-left: 24px;
            margin-right: 10px;
            cursor: pointer;
        }

        .menu-header .logo {
            background-image: url('assets/img/Logo_Module_TiengViet_White.66947422.svg');
            background-repeat: no-repeat;
            width: 96px;
            height: 32px;
            cursor: pointer;
            background-size: contain;
        }

    .menu-list-scroller {
        padding: 0;
        margin: 0;
        width: 100%;
        height: calc(100% - 48px);
        overflow-y: auto;
    }

    .menu-list {
        height: auto;
        padding: 14px 0px 0px;
    }

    .content {
        float: left;
        width: calc(100% - 178px);
        height: 100vh;
        background-color: aqua;
    }

    .content-collapse {
        width: calc(100% - 52px);
    }

        .content .content-header {
            width: 100%;
            height: 48px;
            background-color: #fff;
            display: flex;
            justify-content: space-between;
        }

            .content .content-header .content-header-left {
                height: 100%;
                width: auto;
                display: flex;
                align-items: center;
                justify-content: flex-start;
            }

                .content .content-header .content-header-left .toggle-menu {
                    margin: auto 10px;
                }

                .content .content-header .content-header-left .company {
                    font-size: 14px;
                    padding: 0 0 0 10px;
                    font-weight: 600;
                    color: #111111;
                }

                .content .content-header .content-header-left .company-option {
                    margin-left: 10px;
                    transform: rotate(90deg);
                }

            .content .content-header .content-header-right {
                height: 100%;
                width: auto;
                display: flex;
                justify-content: flex-end;
                margin-right: 20px;
            }

                .content .content-header .content-header-right .notification {
                    height: 100%;
                    width: 44px;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    cursor: pointer;
                    position: relative;
                }

                    .content .content-header .content-header-right .notification .triangle-wrapper {
                        transform: rotate( -19deg );
                        position: absolute;
                        top: 44px;
                        left: 8px;
                    }
                        .content .content-header .content-header-right .notification .triangle-wrapper .triangle {
                            position: absolute;
                            background-color: #000;
                            width: 24px;
                            height: 15px;
                            transform: skew(50deg);
                            display: none;
                        }

                    .content .content-header .content-header-right .notification:hover {
                        background-color: #000000;
                    }

                        .content .content-header .content-header-right .notification:hover .triangle{
                            display: block;
                        }
                        .content .content-header .content-header-right .notification:hover .notification-icon {
                            background-position: -257px -88px;
                        }


                .content .content-header .content-header-right .user {
                    height: 100%;
                    width: auto;
                    display: flex;
                    align-items: center;
                    justify-content: flex-end;
                    padding: 0px 16px;
                    box-sizing: border-box;
                }

                    .content .content-header .content-header-right .user .avatar {
                        margin-right: 5px;
                        border-radius: 50%;
                    }

                    .content .content-header .content-header-right .user .display-user {
                        line-height: 100%;
                        font-size: 14px;
                        font-weight: 400;
                        text-align: right;
                    }

                    .content .content-header .content-header-right .user .user-option {
                        margin-left: 5px;
                    }
</style>
