<template>
    <div class="paging-bar">
        <div class="paging-left">Tổng số: <span>{{totalRecord}}</span> bản ghi</div>
        <div class="paging-right">
            <div class="dropdown-paging">
                <div class="dropdown-main">
                    <p>{{pagingSize}} nhân viên trên trang</p>
                </div>
                <button class="dropdown-choose" @click="showDropdownList" @blur="dropdownPagingOnBlur">
                    <div :class="['dropdown-icon','background-icon','position-icon-down-black', {'rotate-icon':isShowDropdownList}]"></div>
                </button>
                <div v-if="isShowDropdownList" class="dropdown-list" :class="{'dropdown-list-down': pagingSize == 10}">
                    <div v-for="item in pagingSizeList" 
                         :key="item" 
                         :class="['dropdown-item',{'dropdown-item-active': pagingSize == item }]"
                         @click="changePagingSize(item)"
                    >
                    {{item}} bản ghi trên trang
                    </div>
                </div>
            </div>
            <span>
                <button class="previous-btn" :disabled="currentPage==1" @click="changeCurrentPage(currentPage-1)">Trước</button>
                <button v-if="!buttonChooseList.includes(1)" @click="changeCurrentPage(1)" :class="{'button-current-page': 1 == currentPage}">1</button>
                <button v-if="buttonChooseList[0]>2" @click="preButtonChooseList">...</button>
                <button v-for="itemBtn in buttonChooseList" :key="itemBtn" @click="changeCurrentPage(itemBtn)" :class="{'button-current-page': itemBtn == currentPage}">
                {{itemBtn}}
                </button>
                <button v-if="buttonChooseList[buttonChooseList.length-1]<totalPage-1" @click="nextButtonChooseList">...</button>
                <button v-if="!buttonChooseList.includes(totalPage)" @click="changeCurrentPage(totalPage)" :class="{'button-current-page': totalPage == currentPage}">{{totalPage}}</button>
                <button class="next-btn" :disabled="currentPage==totalPage" @click="changeCurrentPage(currentPage+1)">Sau</button>
            </span>
        </div>
    </div>
</template>


<script>
export default {
    name:'PagingBar',
    props:{
        totalRecord: {
            default: 0,
            type: Number
        },
        pagingSize: {
            default: 20,
            type: Number
        },
        currentPage: {
            default: 5,
            type: Number
        }
        },
        data() {
            return {
                pagingSizeList: [10, 20, 30, 50, 100],
                isShowDropdownList: false,
                buttonChooseList: [],
                totalPage: 0
            }
        },
        methods: {
            /**
             * Thực hiện tính toán lại các data của paging từ các prop
             * Author HieuNV
             * */
            reloadPagingBar() {
                // tính tổng số trang
                this.totalPage = Math.ceil(this.totalRecord / this.pagingSize);
                // Tính array buttonChooseList
                if (this.totalPage <= 3) {
                    this.buttonChooseList = []
                    for (let i = 1; i <= this.totalPage; i++) {
                        this.buttonChooseList.push(i);
                    }
                } else {
                    this.buttonChooseList = [];
                    if (this.currentPage <= 3) {
                        this.buttonChooseList = [1, 2, 3];
                    } else if (this.totalPage - this.currentPage <= 2) {
                        let start = this.totalPage - 2;
                        for (let i = start; i <= this.totalPage; i++) {
                            this.buttonChooseList.push(i);
                        }
                    } else {
                        for (let i = this.currentPage - 1; i <= this.currentPage + 1; i++) {
                            this.buttonChooseList.push(i);
                        }
                    }
                }
            },
            /**
             * Sự kiện khi blur dropdown
             * Author HieuNV
             * */
            dropdownPagingOnBlur() {
                setTimeout(() => {
                    this.isShowDropdownList = false;
                },200)
            },
            /**
             * Thực hiện thay đổi pagingSize bằng cách emit ra để truyền lại vào prop, đồng thời chuyển currentPage về 0
             * @param newPagingSize
             * Author HieuNV
             */
            changePagingSize(newPagingSize) {
                this.$emit('changePagingSize', newPagingSize);
                this.$emit('changeCurrentPage', 1);
                this.isShowDropdownList = false;
            },
            /**
             * Thực hiện thay đổi giá trị currentPage
             * @param newCurrentPage
             * Author HieuNV
             */
            changeCurrentPage(newCurrentPage) {
                this.$emit('changeCurrentPage', newCurrentPage);
            },
            /**
             * Sự kiện hiển thị list các pagingSize khi click vào btn option
             * Author HieuNV
             * */
            showDropdownList() {
                this.isShowDropdownList = true;
            },
            /**
             * Sự kiện trượt list button page lên 3 đơn vị
             * Author HieuNV
             * */
            nextButtonChooseList() {             
                if ((this.buttonChooseList[2] + 3) > this.totalPage) {
                    this.buttonChooseList = [];
                    let start = this.totalPage - 2;
                    for (let i = start; i <= this.totalPage; i++) {
                        this.buttonChooseList.push(i);
                    }
                } else {
                    let start = this.buttonChooseList[2] + 1;
                    this.buttonChooseList = [];
                    for (let i = start; i <= start + 2; i++) {
                        this.buttonChooseList.push(i);
                    }
                }
            },
            /**
             *Sự kiện trượt list button page xuống 3 đơn vị
             * Author HieuNV
             * */
            preButtonChooseList() {              
                if (this.buttonChooseList[0] <= 3) {
                    this.buttonChooseList = [];
                    this.buttonChooseList = [1, 2, 3];
                } else {
                    let start = this.buttonChooseList[0] - 3;
                    this.buttonChooseList = [];
                    for (let i = start; i <= start + 2; i++) {
                        this.buttonChooseList.push(i);
                    }
                }
            }
        },
        watch: {
            // mỗi khi 1 trong 3 prop thay đổi thì thực hiện tính toán lại các data để hoàn thiện Paging

            totalRecord() {
                this.reloadPagingBar();
            },
            pagingSize() {
                this.reloadPagingBar();
            },
            currentPage() {
                this.reloadPagingBar();
            },
        },
}
</script>


<style scoped>
    .paging-bar button{
        padding: 0;
        margin: 0;
        cursor: pointer;
        outline: 0;
        background-color: #fff;
        padding: 2px 6px;
    }

    span button{
        border: 1px solid #fff;
    }

    .button-current-page {
        border: 1px solid #e0e0e0 !important;
        font-weight: 700;
    }

        span button:first-child{
            margin-right: 10px;
            margin-left: 15px;
        }

    span button:last-child {
        margin-left: 10px;
        margin-right: 30px;
    }

    .paging-bar {
        width: 100%;
        height: 46px;
        float: left;
        display: flex;
        justify-content: space-between;
        align-items: center;
        border-left: 20px solid #fff;
        border-right: 20px solid #fff;
        box-sizing:border-box;
        background-color: #fff;
    }

    .paging-left{
        line-height: 46px;
        width: auto;
    }

    .paging-left span{
        font-weight: 700;
    }

    .dropdown-paging{
        width: 205px;
        height: 32px;
        position: relative;
    }

    .dropdown-main {
        width: 100%;
        height: 100%;
        border-radius: 2px;
        background-color: #fff;
        border: 1px solid #BABEC5;
        box-sizing: border-box;
    }

    .dropdown-main p{
        line-height: 30px;
        margin: 0;
        padding: 0;
        padding: 0 42px 0 10px;
    }

    .dropdown-choose{
        border:0;
        outline:0;
        background-color: #fff;
        margin: 0;
        padding: 0;
        width: 32px;
        height: 30px;
        position: absolute;
        top: 1px;
        right: 1px;
        border-top-right-radius: 1px;
        border-bottom-right-radius: 1px;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .rotate-icon{
        transform: rotate(180deg);
        transition: 0.1s;
    }

    .dropdown-choose:hover {
        background-color: #E0E0E0;
    }

    .dropdown-list {
        z-index: 21000;
        background-color: #fff;
        width: 100%;
        height: auto;
        padding: 2px 0;
        border: 1px solid #BABEC5;
        border-radius: 2px;
        position: absolute;
        left: 0;
        bottom : 34px;
        box-sizing: border-box;
    }

    .dropdown-list-down{
        top: 34px !important;
        bottom: auto;
    }

    .dropdown-item {
        width: 100%;
        height: 32px;
        line-height: 32px;
        padding: 0 10px;
        box-sizing: border-box;
        cursor: pointer;

    }

        .dropdown-item:hover {
            background-color: #EBEDF0;
            color: #2CA01C;
        }

    .dropdown-item-active {
        background-color: #2CA01C !important;
        color: #fff !important;
    }

    .paging-right{
        width: auto;
        height: 100%;
        display: flex;
        justify-content: flex-end;
        align-items: center;
    }
</style>