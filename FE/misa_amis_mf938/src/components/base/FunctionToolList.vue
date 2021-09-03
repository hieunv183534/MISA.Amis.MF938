<template>
  <div :class="['list-option',{'list-option-hide' : isHide}]" :style="functionListPositionStr">
    <div class="item-option clone-button" @click="btnCloneOnClick">Nhân bản</div>
    <div class="item-option delete-button" @click="btnDeleteOnClick">Xóa</div>
    <div class="item-option stop-button">
      Ngừng sử dụng
    </div>
  </div>
</template>

<script>
    import { EventBus, PopupEventBus, ToastMessengerEventBus } from "../../main.js"
    import EmployeeApi from "../../api/entities/EmployeeApi"
    import ResourceVn from "../../resources/ResourceVn.js"

export default {
        name: 'FunctionToolList',
        data() {
            return {
                isHide: true,
                // style position của components này
                functionListPositionStr: '',
                myEmployeeId: '',
                myEmployeeCode: ''
            }
        },
        methods: {
            /**
             * Sự kiện khi click nhân bản
             * Author HieuNV
             * */
            btnCloneOnClick() {
                this.isHide = true;
                EventBus.$emit('cloneEmployee',this.myEmployeeId);
            },
            /**
             * Sự kiện khi click Xóa
             * Author HieuNV
             * */
            btnDeleteOnClick() {
                this.isHide = true;
                PopupEventBus.$emit('showPopup', 'delete', ResourceVn.PopupMessenger.deleteConfirm(this.myEmployeeCode), 'warning' , 'xw');
            }
        },
        created() {
            EventBus.$on('showFunctionList', (positionStr, employeeId,employeeCode) => {
                this.functionListPositionStr = positionStr;
                this.myEmployeeId = employeeId;
                this.myEmployeeCode = employeeCode;
                this.isHide = false;
            })

            EventBus.$on('hideFunctionList', () => {
                this.isHide = true;
            })

            EventBus.$on('confirmToDelete', () => {
                EmployeeApi.delete(this.myEmployeeId).then(()=> {
                    ToastMessengerEventBus.$emit('showMes', "Đã xóa thành công!", 'success');
                    EventBus.$emit('loadTableData');
                }).catch(error => {
                    console.log(error.response);
                })
            })
        }
}
</script>

<style scoped>
.list-option {
        width: 118px;
        height: auto;
        padding: 2px 1px;
        box-sizing: border-box;
        position: fixed;
        background-color: #fff;
        border: 1px solid #BABEC5;
        border-radius: 2px;
        z-index: 15000;
    }

    .list-option-hide{
        display: none;
    }

    .item-option {
        width: 100%;
        height: 28px;
        float: left;
        font-size: 13px;
        line-height: 28px;
        padding-left: 10px;
        box-sizing: border-box;
        cursor: pointer;
    }

        .item-option:hover {
            background-color: #E8E9EC;
            color: #08BF1E;
        }
</style>