<template>
    <div class="function-tool">
        <div class="edit-button" @click="btnEditOnClick">Sửa</div>
        <button :class="['more-option-button',{'more-option-button-active' : isSelect}]"
                @click="btnMoreOptionOnClick" ref="btnFunction" @blur="btnOptionOnBlur">
            <div class="option-icon background-icon position-icon-down-blue"></div>
        </button>
    </div>
</template>


<script>
    import {EventBus} from "../../main.js"

    export default {
        name: 'Tool',
        props: {
            toolId: String,
            toolCode: String
        },
        data() {
            return {
                isSelect: false
            }
        },
        methods: {
            /**
             * Sưk kiện khi click vào btn more Option
             * Author HieuNV
             * */
            btnMoreOptionOnClick() {
                this.isSelect = true;
                var left = this.$refs.btnFunction.getBoundingClientRect().left - 84;
                var top = this.$refs.btnFunction.getBoundingClientRect().top + 21;
                console.log(document.documentElement.clientHeight);
                if (top + 120 > document.documentElement.clientHeight) {
                    top -= 115;
                }
                var positionStr = "top: " + top + "px; left: " + left + "px;";
                EventBus.$emit('hideFunctionList');
                setTimeout(() => {
                    EventBus.$emit('showFunctionList', positionStr, this.toolId, this.toolCode);
                },200)              
            },
            /**
             * Sự kiện khi blur btn moreOption
             * Author HieuNV
             * */
            btnOptionOnBlur() {
                this.isSelect = false;
                setTimeout(() => {
                    EventBus.$emit('hideFunctionList');
                }, 200)
            },
            /**
             * Sự kiên khi click vào btn sửa
             * Author HieuNV
             * */
            btnEditOnClick() {
                this.$emit('btnEditOnClick', this.toolId);
            }
        }
    }
</script>


<style scoped>
    .function-tool {
        width: 80px;
        height: 36px;
        float: left;
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        margin-top: 4px;
        margin-left: 10px;
    }

    .edit-button {
        width: auto;
        height: 14px;
        line-height: 13px;
        font-size: 13px;
        color: #0075c0;
        margin-right: 9px;
        box-sizing: border-box;
        cursor: pointer;
        border: 1px solid transparent;
        font-weight: 600;
    }

        .edit-button:active {
            border: 1px solid #0075c0;
        }


    .more-option-button {
        border: 0;
        outline: 0;
        margin: 0;
        padding: 0;
        width: 26px;
        height: 18px;
        display: flex;
        justify-content: center;
        box-sizing: border-box;
        cursor: pointer;
        border: 1px solid transparent;
        background-color: transparent;
    }

    .more-option-button-active {
        border: 1px solid #0075c0;
    }

    .more-option-button:active {
        border: 1px solid #0075c0;
    }
</style>