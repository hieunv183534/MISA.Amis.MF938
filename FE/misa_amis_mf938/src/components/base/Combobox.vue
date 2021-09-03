<template>
    <div class="combobox">
        <input :class="['combobox-search', {'border-red': isBorderRed}]" type="text" v-model="comboboxText"
               @keyup.enter="searchCombobox" ref="inputCombobox" @blur="cbbOnBlur"
               v-on:keyup.up="upSelect" v-on:keyup.down="downSelect" :title="inputTitle" />
        <button class="combobox-option" @click="comboboxOptionOnClick">
            <div class="combobox-icon background-icon position-icon-down-black"></div>
        </button>
        <div v-if="isShowData" class="combobox-data">
            <div class="data-header">
                <p class="unit-code">Mã đơn vị</p>
                <p class="unit-name">Tên đơn vị</p>
            </div>
            <div class="combobox-item" v-for="item in listItemShow" :key="item.unitId"
                 value="item.UnitId" @click="comboboxItemOnClick(item.UnitId)"
                 :class="{ 'item-selected': item.UnitId == comboboxValue }">
                <p class="unit-code">{{item.UnitCode}}</p>
                <p class="unit-name">{{item.UnitName}}</p>
            </div>
        </div>
    </div>
</template>


<script>
    export default {
        name: 'Combobox',
        props: {
            // có hiển thị list item hay không
            isShowData: {
                type: Boolean,
                default: false,
            },
            // ds các item để lựa chọn
            listItem: {
                default: []
            },
            // giá trị của component này
            comboboxValue: {
                type: String,
                default: null
            },
            // kiểm soát việc chọn lại
            reSelect: {
                type: Boolean,
                default: false
            },
            // border của input có đỏ hay không
            isBorderRed: {
                type: Boolean,
                default: false
            },
            //title
            inputTitle: String
        },
        data() {
            return {
                listItemShow: [],
                comboboxText: "",
                // index của item được chon trong list. dùng khi keyup.up, keyup.down
                index: -1
            }
        },
        created() {
            this.listItemShow = this.listItem;
        },
        watch: {
            // khi nhận list item mới thì gán ngay cho list show để có thể show ra
            listItem() {
                this.listItemShow = this.listItem;
            },
            //khi nhận được sự thay dổi reSelect => người dùng chọn lại => thay đổi text
            reSelect() {
                this.listItem.forEach((item) => {
                    if (item.UnitId == this.comboboxValue) {
                        this.comboboxText = item.UnitName;
                    }
                });
            }
        },
        methods: {
            /**
             * Xử lí tìm kiếm các item hợp lệ với searchTerms để thêm vào listShow
             * Author HieuNV
             * */
            searchCombobox() {
                var searchTerms = this.comboboxText;
                this.listItemShow = [];
                this.listItem.forEach((item) => {
                    if (item.UnitCode.toLowerCase().includes(searchTerms.toLowerCase())
                        || item.UnitName.toLowerCase().includes(searchTerms.toLowerCase())) {
                        this.listItemShow.push(item);
                    }
                });
                if (this.listItemShow.length > 0) {
                    this.$emit('comboboxOnSelect', this.listItemShow[0].UnitId);
                    this.index = 0;
                }
            },
            /**
             * Sự kiện khi click vào một item trong list
             * @param id
             * Author HieuNV
             */
            comboboxItemOnClick(id) {
                this.$emit('comboboxOnSelect', id)
                for (let i = 0; i < this.listItemShow.length; i++) {
                    if (id == this.listItemShow.[i].UnitId) {
                        this.index = i;
                    }
                }
            },
            /**
             * Sự kiện khi click vào btn Option
             * Author HieuNV
             * */
            comboboxOptionOnClick() {
                this.$refs.inputCombobox.focus();
                setTimeout(() => {
                    this.$emit('showDataCombobox');
                }, 300)
                this.listItemShow = [];
                this.listItem.forEach((item) => {
                    this.listItemShow.push(item);
                });
                for (let i = 0; i < this.listItemShow.length; i++) {
                    if (this.comboboxValue == this.listItemShow.[i].UnitId) {
                        this.index = i;
                    }
                }
            },
            /**
             * Sự kiện khi blur cbb
             * */
            cbbOnBlur() {
                setTimeout(() => {
                    this.$emit('comboboxOnBlur');
                }, 200)
            },
            /**
             * Sự kiện khi keyup.up
             * Author HieuNV
             * */
            upSelect() {
                console.log('upup')
                if (this.index > 0) {
                    this.index--;
                    console.log(this.index);
                    this.$emit('comboboxOnSelect', this.listItemShow.[this.index].UnitId);
                }
            },
            /**
             * Sự kiện khi keyup.down
             * Author HieuNV
             * */
            downSelect() {
                console.log('down')
                if (this.index < this.listItemShow.length - 1) {
                    this.index++;
                    this.$emit('comboboxOnSelect', this.listItemShow.[this.index].UnitId);
                }
            }
        }
    }
</script>


<style scoped>
    .combobox {
        width: 405px;
        height: 32px;
        position: relative;
        float: left;
    }

    .combobox-search {
        outline: 0;
        border: 0;
        width: 405px;
        height: 32px !important;
        border-radius: 2px;
        border: 1px solid #BABEC5;
        padding: 5px 42px 5px 10px;
        box-sizing: border-box;
    }

    .border-red {
        border-color: #ff0000 !important;
    }

    .combobox-search:focus {
        border-color: #2C9F1C;
    }

    .combobox-search:focus {
        border-color: #2CA01C;
    }

    .combobox-option {
        border: 0;
        outline: 0;
        width: 32px;
        height: 30px;
        cursor: pointer;
        display: flex;
        align-items: center;
        justify-content: center;
        position: absolute;
        top: 1px;
        right: 1px;
        z-index: 5;
    }

        .combobox-option:hover {
            background-color: #E0E0E0;
        }

        .combobox-option:focus {
            background-color: #E0E0E0;
        }

    .combobox-data {
        position: absolute;
        top: 35px;
        left: 0;
        width: 100%;
        height: auto;
        border: 1px solid #BABEC5;
        border-radius: 2px;
        box-sizing: border-box;
        z-index: 2000;
        background-color: #fff;
        box-shadow: 1px 2px 4px 1px #454545;
    }

    .data-header {
        height: 32px;
        width: 100%;
        background-color: #F4F5F8;
    }

    .unit-code {
        font-size: 13px;
        display: block;
        margin: 0;
        float: left;
        height: 100%;
        width: 100px;
        padding-left: 10px;
        line-height: 32px;
        box-sizing: border-box;
    }

    .unit-name {
        display: block;
        margin: 0;
        float: left;
        height: 100%;
        width: calc( 100% - 100px );
        padding-left: 10px;
        line-height: 32px;
        font-size: 13px;
        box-sizing: border-box;
    }

    .data-header .unit-code {
        font-weight: 600;
    }

    .data-header .unit-name {
        font-weight: 600;
    }

    .combobox-item {
        width: 100%;
        height: 32px;
        float: left;
        cursor: pointer;
    }

        .combobox-item:hover {
            background-color: #EBEDF0 !important;
            color: #35BF50 !important;
        }

    .item-selected {
        background-color: #EBEDF0 !important;
        color: #35BF50 !important;
    }
</style>