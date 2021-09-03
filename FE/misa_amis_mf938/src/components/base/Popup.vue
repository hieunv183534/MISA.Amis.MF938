<template>
    <div class="popup">
        <div class="popup-modal"></div>
        <div class="popup-content">
            <div class="popup-body">
                <div v-if="popupType == 'warning'" class="popup-icon background-icon position-icon-warning"></div>
                <div v-if="popupType == 'question'" class="popup-icon background-icon position-icon-question"></div>
                <div v-if="popupType == 'danger'" class="popup-icon background-icon position-icon-danger"></div>
                <p class="popup-text">{{popupText}}</p>
            </div>
            <div :class="['popup-footer',{'display-center' : popupListButton == 't', 'display-end' : popupListButton == 'u'}]">
                <Button v-if="popupListButton.includes('z')" buttonText="Hủy"
                        buttonClass="button-secondary" @btnClick="$emit('popupOnConfirm','z')" />
                <Button v-if="popupListButton.includes('w')" buttonText="Không"
                        buttonClass="button-secondary" @btnClick="$emit('popupOnConfirm','w')" />
                <div class="btns-wrapper">
                    <Button v-if="popupListButton.includes('y')" buttonText="Không"
                            buttonClass="button-secondary" @btnClick="$emit('popupOnConfirm','y')" />
                    <Button v-if="popupListButton.includes('x')" buttonText="Có"
                            buttonClass="button-primary" @btnClick="$emit('popupOnConfirm','x')" />
                </div>
                <Button v-if="popupListButton == 't'" buttonText="Đóng"
                        buttonClass="button-primary" @btnClick="$emit('popupOnConfirm','t')" />
                <Button v-if="popupListButton == 'u'" buttonText="Đồng ý"
                        buttonClass="button-primary" @btnClick="$emit('popupOnConfirm','u')" />
            </div>
        </div>
    </div>
</template>


<script>
    import Button from "./Button.vue"
    export default {
        name: 'Popup',
        components: {
            Button
        },
        props: {
            popupType: String,
            popupText: String,
            // các button sẽ được sử dụng trong popup hiện tại
            // ds các button: x,y,z,t,w,u
            popupListButton: {
                type: String,
                default: ''
            }
        }
    }
</script>


<style scoped>
    .popup-modal {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: #000;
        opacity: 0.4;
        z-index: 19999;
    }

    .popup-content {
        z-index: 20000;
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 444px;
        height: auto;
        background-color: #fff;
        padding: 32px;
        box-sizing: border-box;
        border-radius: 3px;
    }

    .popup-body{
        width:100%;
        height: calc( 100% - 67px);
        overflow-y:hidden;
        margin-bottom: 10px;
    }

    .popup-icon{
        margin-right: 16px;
        float: left;
    }

    .popup-text {
        margin: 0;
        float: left;
        display: block;
        width: calc(100% - 64px);
        height: auto;
        text-align: left;
        font-size: 13px;
        margin-top: 10px;
    }


    .popup-footer {
        width: 100%;
        height: 57px;
        box-sizing: border-box;
        display: flex;
        justify-content: space-between;
        align-items: flex-end;
        border-top: 1px solid #E0E0E0;
    }

    .btns-wrapper {
        height: auto;
        width: auto;
        display: flex;
    }

    .btns-wrapper button:last-child{
        margin-left: 10px;
    }

    .display-center{
        justify-content:center !important;
    }

    .display-end {
        justify-content: flex-end !important;
    }

</style>