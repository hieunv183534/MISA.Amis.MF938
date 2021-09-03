<template>
    <div :class="['toast-messenger',
         {'border-warning':mesType == 'warning', 
         'border-danger':mesType == 'danger', 
         'border-question':mesType == 'question',
         'border-success':mesType == 'success',
         'mes-show': isShowMes,
         'mes-hide': !isShowMes}]" >
        <div v-if="mesType == 'warning'" class="mes-icon background-icon position-icon-warning"></div>
        <div v-if="mesType == 'question'" class="mes-icon background-icon position-icon-question"></div>
        <div v-if="mesType == 'danger'" class="mes-icon background-icon position-icon-danger"></div>
        <div v-if="mesType == 'success'" class="mes-icon background-icon position-icon-success"></div>
        <div class="mes-content">{{mesText}}</div>
    </div>
</template>


<script>
export default {
    name:'ToastMessenger',
    props:{
        mesType: {
            type: String,
            default: ''
        },
        mesText: {
            type: String,
            default: ''
        },
        isShowMes: {
            type: Boolean,
            default: false
        }
        },
        watch: {
            // mỗi khi toastMessenger hiện lên thì cho ẩn đi sau đúng 3s
            isShowMes() {
                if (this.isShowMes == true) {
                    setTimeout(() => {
                        this.$emit('hideMes');
                    }, 3000);
                }
            }
        }
}
</script>


<style scoped>

    .toast-messenger {
        width: 400px;
        height: 70px;
        box-sizing: border-box;
        display: flex;
        align-items: center;
        justify-content: flex-start;
        padding: 0 16px;
        z-index: 34000;
        position: fixed;
        top: 10px;
        background-color: #fff;
        border-radius: 8px;
        box-shadow: 0px 3px 6px 0px rgba(0, 0, 0, 0.16);
    }

    .border-success {
        border: 1px solid #2CA01C;
    }

    .mes-content{
        margin-left: 5px;
    }

    .mes-show {
        right: 5px;
        opacity: 1;
        transition: 0.7s;
    }

    .mes-hide {
        right: -405px;
        opacity: 0.1;
        transition: 0.7s;
    }

</style>