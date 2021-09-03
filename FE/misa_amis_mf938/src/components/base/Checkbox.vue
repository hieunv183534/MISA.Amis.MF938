<template>
    <button class="checkbox-wrapper"  @click="clickCheckBox" >
        <div class="checkbox" :class="{'checkbox-check': isCheck }">
            <div v-if="isCheck" class="check-icon background-icon position-icon-check-active">
            </div>
        </div>
    </button>
</template>

<script>
export default {
    name: 'Checkbox',
    data(){
        return{
            isCheck: false,
        }
    },
    props:{
        chechOrNo: Boolean
    },
        methods: {
        /**
         * Sự kiện khi click vào checkbox
         * Author HieuNV
         * */
        clickCheckBox(){
            this.$emit('checkboxOnClick');

            // thay đổi trang thái
            this.isCheck = !this.isCheck;
        }
    },
        watch: {
        /**
         * khi bên ngoài truyền vào qua checkOrNo thì lấy để gán cho data isCheck
         * */
        chechOrNo(){
            this.isCheck = this.chechOrNo;
        }
    }
}
</script>

<style scoped>

    .checkbox-wrapper{
        padding: 0;
        border: 0;
        outline: 0;
        background-color: transparent;
        width: 24px;
        height: 24px;
        display: flex;
        justify-content: center;
        align-items: center;
        box-sizing: border-box;
        border: 0;
        float: left;
        border-radius: 2px;
    }

    .checkbox {
        width: 18px;
        height: 18px;
        border: 1px solid #AFAFAF;
        position: relative;
        border-radius: 2px;
        box-sizing: border-box;
        background-color: #fff;
    }

    .check-icon{
        position: absolute;
        top: 0px;
        left: 0px;
    }


    .checkbox-check{
    border: 1px solid #2CA01C;
}



    .checkbox-wrapper:focus{
        border: 1px solid #A4DFFF;
    }

</style>