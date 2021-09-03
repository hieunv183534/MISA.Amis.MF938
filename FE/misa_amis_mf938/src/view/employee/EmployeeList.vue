<template>
    <div class="employee-content">
        <div class="employee-header">
            <div class="header-row">
                <p class="title-page">Nhân viên</p>
                <Button buttonText="Thêm mới nhân viên" buttonClass="button-primary" @btnClick="btnAddOnClick"/>
            </div>
        </div>
        <div class="employee-toolbar">
            <div class="toolbar-row">
                <div class="toolbar-left">
                    <button  :disabled="checkedEmployees.length < 2" 
                            :class="['batch-function-button',{'btn-batch-disable':checkedEmployees.length < 2}]" 
                            @click="btnBatchOnClick" @blur="btnBatchOnBlur">
                        <p>Thực hiện hàng loạt</p>
                        <div :class="['dropdown-icon','background-icon',{'position-icon-down-xam':checkedEmployees.length < 2,'position-icon-down-black':checkedEmployees.length >= 2} ]"></div>
                        <div v-if="isShowListBatch && checkedEmployees.length >= 2" class="batch-list">
                            <div class="delete-batch" @click="deleteBatch">Xóa</div>
                        </div>
                    </button>
                </div>
                <div class="toolbar-right">
                    <div class="input-search-wrapper">
                        <Input inputClass="input-search"
                               inputType="text"
                               inputPlacehoder="Tìm theo mã, tên nhân viên, số điện thoại"
                               :inputValue="searchTerms"
                               v-model="searchTerms"
                               @inputOnEnter="enterSearch" />
                        <div class="icon-search background-icon position-icon-search"></div>
                    </div>
                    <div class="btn-refresh background-icon position-icon-refresh" @click="btnRefreshOnClick"></div>
                    <div class="btn-export background-icon position-icon-export-excel" @click="btnExportOnClick"></div>
                </div>
            </div>
        </div>
        <div class="table-paging">
            <Table :tableColumns="tableColumns" :tableDataList="tableDataList" @chooseAnEmployee="chooseAnEmployee" v-model="checkedEmployees"/>
            <PagingBar :totalRecord="totalRecord" :pagingSize="pagingSize" :currentPage="currentPage"
                       @changePagingSize="changePagingSize" @changeCurrentPage="changeCurrentPage" />
        </div>
        <EmployeeDialog v-if="isShowDialog" @hideDialog="hideDialog" :isReOpen="isReOpenDialog" 
                        :mode="dialogMode" :myEmployeeId="myEmployeeId" :isCloneMode="isCloneMode"/>
    </div>
</template>

<script>
    import {EventBus, LoaderEventBus} from "../../main.js"
    import Button from "../../components/base/Button.vue";
    import Input from "../../components/base/Input.vue";
    import Table from "../../components/base/Table.vue";
    import PagingBar from "../../components/base/PagingBar.vue";
    import EmployeeDialog from "./EmployeeDialog.vue";
    import { employeeColumns } from "./EmployeeColumns";
    import EmployeeApi from "../../api/entities/EmployeeApi"

    export default {
        name: "EmployeeList",
        components: {
            Button,
            Input,
            EmployeeDialog,
            Table,
            PagingBar
        },
        data() {
            return {
                myEmployeeId:'',
                //-------------------------------
                // các mảng cột và hàng truyền vào cho table
                tableColumns: employeeColumns,
                tableDataList: [],
                //-------------------------------------------
                // các biến lưu dữ liệu của paging
                totalRecord: 0,
                currentPage: 1,
                pagingSize: 20,
                // lưu giá trị nhận được từ inputSearch
                searchTerms: '',
                // các biến liên quan đến xử lí với dialog
                isShowDialog: false,
                isReOpenDialog: false,
                dialogMode: '',
                isCloneMode: false,
                // list id employee được chọn
                checkedEmployees: [],
                isShowListBatch: false
            };
        },
        created() {
            this.loadDataTablePaging();

            // lắng nghe sự kiện ẩn dialog
            EventBus.$on('hideDialog', () => {
                this.isShowDialog = false;
            })

            // lắng nghe sự kiên load dữ liệu table
            EventBus.$on('loadTableData', () => {
                this.loadDataTablePaging();
            })

            // lắng nghe sự kiện nhân bản nhân viên
            EventBus.$on('cloneEmployee', (employeeId) => {
                this.isShowDialog = true;
                setTimeout(() => {
                    this.isCloneMode = true;
                    this.isReOpenDialog = !this.isReOpenDialog;
                    this.dialogMode = 'add';
                    this.myEmployeeId = employeeId;
                }, 100);
            })
        },
        methods: {
            btnBatchOnClick() {
                this.isShowListBatch = true;
            },
            btnBatchOnBlur() {
                setTimeout(() => {
                    this.isShowListBatch = false;
                },200)
            },
            deleteBatch() {
                this.isShowListBatch = false;
                console.log(JSON.stringify(this.checkedEmployees));
                EmployeeApi.deleteBatch(JSON.stringify(this.checkedEmployees)).then((res) => {
                    console.log("Xóa thành công " + res.data + "  bản ghi");
                })
            },
            /**
             * Thay đổi pagingSize
             * @param newPagingSize
             * Author HieuNV
             */
            changePagingSize(newPagingSize) {
                this.pagingSize = newPagingSize;
                this.loadDataTablePaging();
            },
            /**
             * Thay đổi currentPage
             * @param newCurentPage
             * Author HieuNV
             */
            changeCurrentPage(newCurentPage) {
                this.currentPage = newCurentPage;
                this.loadDataTablePaging();
            },
            /**
             * Từ searchTerms, pagingSize, currentPage => load ra query tương ứng để gọi api filter
             * Author HieuNv
             * */
            getQueryStringFilter() {
                var paramStrs = `pageSize=${this.pagingSize}&pageNumber=${this.currentPage}`
                if (this.searchTerms !== undefined && this.searchTerms !== '') {
                    paramStrs += `&searchTerms=${this.searchTerms}`;
                }
                return paramStrs;
            },
            /**
             * Gọi api filter để thực hiện lấy dữ liệu đã được tìm kiếm và phân trang, 
             * nhận res.data là list employee truyền cho Table và ToltalRecord để truyền cho pagingBar
             * Author HieuNV
             * */
            loadDataTablePaging() {
                LoaderEventBus.$emit('showLoader');
                var vm = this;
                EmployeeApi.getFilterPaging(this.getQueryStringFilter()).then((res) => {
                    vm.tableDataList = res.data.Data;
                    vm.totalRecord = res.data.TotalRecord;
                    LoaderEventBus.$emit('hideLoader');
                })
            },
            /**
             * Thực hiện tìm kiếm khi enter input search
             * Author HieuNV
             * */
            enterSearch() {
                console.log(this.searchTerms);
                this.currentPage = 1;
                this.loadDataTablePaging();
            },
            /**
             * Thực hiện load lại dữ liệu khi click btn refresh, reset lại this.currentPage = 1; this.pagingSize = 20;
             * Author HieuNV
             * */
            btnRefreshOnClick() {
                this.currentPage = 1;
                this.pagingSize = 20;
                this.searchTerms = ''
                this.loadDataTablePaging();
            },
            /**
             * Thực hiện gọi api export và thực hiện tự động download file excel nhận được từ res api
             * Author HieuNV
             * */
            btnExportOnClick() {
                EmployeeApi.getExport().then((res) => {
                    const url = window.URL.createObjectURL(new Blob([res.data]));
                    const a = document.createElement("a");
                    a.href = url;
                    const filename = `file.xlsx`;
                    a.setAttribute("download", filename);
                    document.body.appendChild(a);
                    a.click();
                    a.remove();
                })
            },
            /**
             * Xử lí mở dialog khi click vào btn thêm mới nhân viên
             * Author HieuNV
             * */
            btnAddOnClick() {
                this.isShowDialog = true;
                setTimeout(() => {
                    this.isReOpenDialog = !this.isReOpenDialog;
                    this.dialogMode = 'add';
                    this.isCloneMode = false;
                },100)
            },
            /**
             * Ẩn Dialog
             * Author HieuNV
             * */
            hideDialog() {
                this.isShowDialog = false;
            },
            /**
             * Nhận sự kiện khi table chọn 1 nhân viên và truyền lên kèm theo id nhân viên => mở dilalog sửa
             * @param employeeId
             * Author HieuNV
             */
            chooseAnEmployee(employeeId) {
                this.isShowDialog = true;
                this.dialogMode = 'update';
                setTimeout(() => {
                    this.isReOpenDialog = !this.isReOpenDialog;
                }, 100)
                this.myEmployeeId = employeeId;
            }
        }
    };
</script>

<style scoped>
    .employee-content {
        background-color: #f4f5f8;
        float: left;
        width: 100%;
        height: calc(100% - 48px);
        padding-left: 20px;
        padding-right: 30px;
        box-sizing: border-box;
    }

        .employee-content .employee-header {
            background-color: #f4f5f8;
            width: 100%;
            height: 84px;
            padding: 24px 0px 24px 0px;
            box-sizing: border-box;
            float: left;
        }

            .employee-content .employee-header .header-row {
                width: 100%;
                height: 36px;
                color: #111111;
                display: flex;
                align-items: center;
                justify-content: space-between;
            }

            .employee-content .employee-header .header-row {
                line-height: 36px;
                font-weight: 700;
                font-size: 24px;
            }

    .employee-toolbar {
        width: 100%;
        height: 68px;
        padding: 16px 20px;
        box-sizing: border-box;
        background-color: #fff;
        float: left;
    }

        .employee-toolbar .toolbar-row {
            width: 100%;
            height: 36px;
            display: flex;
            justify-content: space-between;
            
        }

        .toolbar-left{
            height: 100%;
            width: auto;
        }

    .batch-function-button {
        width: auto;
        height: 100%;
        background-color: #fff;
        border: 1px solid #8D9096;
        box-sizing: border-box;
        border-radius: 30px;
        padding: 0px 16px;
        align-items: center;
        display: flex;
        justify-content: flex-start;
        position: relative;
        cursor: pointer;
    }


    .btn-batch-disable {
        border: 2px solid #B1B2B3;
    }

    .batch-function-button p {
        font-weight: 600;
        line-height: 36px;
    }

        .batch-function-button .dropdown-icon {
            margin-left: 5px;
        }

    .batch-list {
        position: absolute;
        top: 38px;
        right: 0;
        width: 104px;
        height: auto;
        padding: 1px 2px;
        box-sizing: border-box;
        border: 1px solid #B1B2B3;
        border-radius: 3px;
        z-index: 32333;
        background-color: #fff;
    }

        .delete-batch{
            width: 100%;
            height: 28px;
            line-height: 28px;
            display: flex;
            align-items: center;
            padding-left: 10px;
            cursor: pointer;
            box-sizing: border-box;
        }

            .delete-batch:hover {
                background-color: #E8E9EC;
            }

            .toolbar-right {
                height: 100%;
                width: auto;
                display: flex;
                align-items: flex-end;
                justify-content: flex-end;
            }

    .input-search-wrapper {
        width: 305px;
        height: 32px;
        position: relative;
        margin-right: 10px;
    }

        .input-search-wrapper .icon-search {
            position: absolute;
            top: 8px;
            right: 8px;
        }

    .btn-refresh {
        margin: 0px 6px;
        cursor: pointer;
    }

        .btn-refresh:hover {
            background-position: -1096px -88px;
        }

    .btn-export {
        cursor: pointer;
        margin-left: 6px;
    }

        .btn-export:hover {
            background-position: -705px -257px;
        }

    .table-paging {
        width: 100%;
        /*height: auto;*/
        height: calc(100% - 152px);
        background-color: #F4F5F8;
        /*padding-right: 20px;*/
        /*padding-left: 20px;*/
        box-sizing: border-box;
        float: left;
    }
</style>