<template>
    <div class="rb-group">
        <RadioButton @radioButtonOnClick="rb1OnClick" :rbValue="rb1Value" />
        <p>Nam</p>
        <RadioButton @radioButtonOnClick="rb0OnClick" :rbValue="rb0Value" />
        <p>Nữ</p>
        <RadioButton @radioButtonOnClick="rb2OnClick" :rbValue="rb2Value" />
        <p>Khác</p>
    </div>
</template>


<script>
    import RadioButton from "./RadioButton.vue"

    export default {
        name: 'Name',
        props: {
            rbgValue: Number
        },
        components: {
            RadioButton
        },
        data() {
            return {
                rb0Value: false,
                rb1Value: false,
                rb2Value: false,
            }
        },
        methods: {
            /**
             * Sự kiện khi click vào rb0
             * Author HieuNV
             * */
            rb0OnClick() {
                if (this.rb0Value == false) {
                    this.rb0Value = true;
                    this.rb1Value = false;
                    this.rb2Value = false;
                } else {
                    this.rb0Value = false
                }
                this.$emit('input', this.getValue())
            },
            /**
             * Sự kiện khi click vào rb1
             * Author HieuNV
             * */
            rb1OnClick() {
                if (this.rb1Value == false) {
                    this.rb0Value = false;
                    this.rb1Value = true;
                    this.rb2Value = false;
                } else {
                    this.rb1Value = false;
                }
                this.$emit('input', this.getValue())
            },
            /**
             * Sự kiện khi click vào rb2
             * Author HieuNV
             * */
            rb2OnClick() {
                if (this.rb2Value == false) {
                    this.rb0Value = false;
                    this.rb1Value = false;
                    this.rb2Value = true;
                } else {
                    this.rb2Value = false;
                }
                this.$emit('input', this.getValue())
            },
            /**
             * Lấy giá trị của rb group hiện tại thông qua giá trị các rb
             * Author HieuNV
             * */
            getValue() {
                if (this.rb0Value == true) {
                    return 0;
                } else if (this.rb1Value == true) {
                    return 1;
                } else if (this.rb2Value == true) {
                    return 2;
                } else {
                    return null;
                }
            }
        },
        watch: {
            // khi thay đổi giá trị của rb-group thì thay đổi giá trị của các rb thành phần cho hợp lí
            rbgValue() {
                switch (this.rbgValue) {
                    case 0:
                        this.rb0Value = true;
                        break;
                    case 1:
                        this.rb1Value = true;
                        break;
                    case 2:
                        this.rb2Value = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
</script>


<style scoped>
    .rb-group {
        height: 32px;
        width: 225px;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

        .rb-group p {
            font-size: 13px;
            line-height: 32px;
            margin-right: 20px;
            margin-left: 10px;
            margin-top:0;
            margin-bottom:0;
        }
</style>