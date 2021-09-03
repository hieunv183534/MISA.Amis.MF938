<template>
    <div class="dialog">
        <div class="dialog-modal"></div>
        <div class="dialog-content">
            <div class="dialog-header">
                <p class="dialog-title">Thông tin nhân viên</p>
                <Checkbox />
                <p class="choose-user">Là khách hàng</p>
                <Checkbox />
                <p class="choose-user">Là nhà cung cấp</p>
                <div class="help-btn background-icon position-icon-help"></div>
                <div class="x-btn background-icon position-icon-x" @click="btnXOnClick"></div>
            </div>
            <div class="dialog-body">
                <div class="dialog-up">
                    <div class="dialog-row">
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Mã <font color="red">*</font></p>
                                <Input inputType="text"
                                       inputClass="input-w-2"
                                       :isFocus="changeFocus"
                                       :isBorderRed="isEmployeeCodeValidate"
                                       :inputTitle="inputEmployeeCodeTitle"
                                       v-model="employeeModel.EmployeeCode"
                                       :inputValue="employeeModel.EmployeeCode"
                                       @inputOnBlur="validateRequied('EmployeeCode')" />
                            </div>
                            <div class="prop-item">
                                <p>Tên <font color="red">*</font></p>
                                <Input inputType="text"
                                       inputClass="input-w-4"
                                       :isBorderRed="isFullNameValidate"
                                       :inputTitle="inputFullNameTitle"
                                       v-model="employeeModel.FullName"
                                       :inputValue="employeeModel.FullName"
                                       @inputOnBlur="validateRequied('FullName')" />
                            </div>
                        </div>
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Ngày sinh</p>
                                <Input inputType="date"
                                       inputClass="input-w-2"
                                       v-model="employeeModel.DateOfBirth"
                                       :inputValue="formatDateToValue(employeeModel.DateOfBirth)" />
                            </div>
                            <div class="prop-item">
                                <p>Giới tính</p>
                                <RadioButtonGroup v-model="employeeModel.Gender"
                                                  :rbgValue="employeeModel.Gender" />
                            </div>
                        </div>
                    </div>
                    <div class="dialog-row">
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Đơn vị <font color="red">*</font></p>
                                <Combobox :listItem="comboboxList"
                                          :comboboxValue="employeeModel.UnitId"
                                          :isShowData="isShowDataCombobox"
                                          :isBorderRed="isUnitIdValidate"
                                          :inputTitle="inputUnitIdTitle"
                                          @comboboxOnSelect="comboboxOnSelect"
                                          @showDataCombobox="showDataCombobox"
                                          @comboboxOnBlur="comboboxOnBlur"
                                          :reSelect="reSelectCbb" />
                            </div>
                        </div>
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Số CMND</p>
                                <Input inputType="text"
                                       inputClass="input-w-4"
                                       v-model="employeeModel.IdentityNumber"
                                       :inputValue="employeeModel.IdentityNumber" />
                            </div>
                            <div class="prop-item">
                                <p>Ngày cấp</p>
                                <Input inputType="date"
                                       inputClass="input-w-2"
                                       v-model="employeeModel.IdentityDate"
                                       :inputValue="formatDateToValue(employeeModel.IdentityDate)" />
                            </div>
                        </div>
                    </div>
                    <div class="dialog-row">
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Chức danh</p>
                                <Input inputType="text"
                                       inputClass="input-w-6"
                                       v-model="employeeModel.Position"
                                       :inputValue="employeeModel.Position" />
                            </div>
                        </div>
                        <div class="hatf-row-dialog">
                            <div class="prop-item">
                                <p>Nơi cấp</p>
                                <Input inputType="text"
                                       inputClass="input-w-6"
                                       v-model="employeeModel.IdentityPlace"
                                       :inputValue="employeeModel.IdentityPlace" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="dialog-down">
                    <div class="dialog-row">
                        <div class="prop-item">
                            <p>Địa chỉ</p>
                            <Input inputType="text"
                                   inputClass="input-w-12"
                                   v-model="employeeModel.Address"
                                   :inputValue="employeeModel.Address" />
                        </div>
                    </div>
                    <div class="dialog-row-lite">
                        <div class="prop-item">
                            <p>Điện thoại di động</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   v-model="employeeModel.MobilePhone"
                                   :inputValue="employeeModel.MobilePhone" />
                        </div>
                        <div class="prop-item">
                            <p>Điện thoại cố định</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   v-model="employeeModel.DesktopPhone"
                                   :inputValue="employeeModel.DesktopPhone" />
                        </div>
                        <div class="prop-item">
                            <p>Email</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   :isBorderRed="isEmailValidate"
                                   :inputTitle="inputEmailTitle"
                                   v-model="employeeModel.Email"
                                   :inputValue="employeeModel.Email"
                                   @inputOnBlur="validateEmail" />
                        </div>
                    </div>
                    <div class="dialog-row-lite">
                        <div class="prop-item">
                            <p>Tài khoản ngân hàng</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   v-model="employeeModel.BankAccount"
                                   :inputValue="employeeModel.BankAccount" />
                        </div>
                        <div class="prop-item">
                            <p>Tên ngân hàng</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   v-model="employeeModel.BankName"
                                   :inputValue="employeeModel.BankName" />
                        </div>
                        <div class="prop-item">
                            <p>Chi nhánh</p>
                            <Input inputType="text"
                                   inputClass="input-w-3"
                                   v-model="employeeModel.BankBranch"
                                   :inputValue="employeeModel.BankBranch" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="dialog-footer">
                <Button buttonText="Hủy"
                        buttonClass="button-secondary" @btnClick="hideDialog" />
                <div class="btns-wrapper">
                    <Button buttonText="Cất"
                            buttonClass="button-secondary"
                            @btnClick="btnSaveOnClick" />
                    <Button buttonText="Cất và thêm"
                            buttonClass="button-primary"
                            @btnClick="btnSaveAndAddOnClick" />
                </div>
            </div>
        </div>
    </div>
</template>


<script>
    import { PopupEventBus, EventBus, ToastMessengerEventBus } from "../../main.js"
    import Checkbox from "../../components/base/Checkbox.vue"
    import Button from "../../components/base/Button.vue"
    import Input from "../../components/base/Input.vue"
    import Combobox from "../../components/base/Combobox.vue"
    import RadioButtonGroup from "../../components/base/RadioButtonGroup.vue"
    import { EmployeeModel } from "../../models/EmployeeModel.js"
    import EmployeeApi from "../../api/entities/EmployeeApi"
    import UnitApi from "../../api/entities/UnitApi"
    import FormatData from "../../utils/Format.js"
    import Validation from "../../utils/Validation.js"
    import ResourceVn from "../../resources/ResourceVn"

    export default {
        name: 'EmployeeDialog',
        components: {
            Checkbox, Button, Input, Combobox, RadioButtonGroup
        },
        props: {
            mode: {
                type: String,
                default: ''
            },
            myEmployeeId: {
                type: String,
                default: ''
            },
            isReOpen: {
                type: Boolean,
                default: false
            },
            isShowDialog: {
                type: Boolean,
                default: false
            },
            isCloneMode: {
                type: Boolean,
                default: false
            }
        },
        data() {
            return {
                // obj abn đầu khi mới reOpen form. để so sánh changed
                originalModel: {},
                // mảng các thuộc tính requied
                requiedList: ['EmployeeCode', 'FullName', 'UnitId'],
                // các biến sử dụng cho combobox
                comboboxList: [],
                isShowDataCombobox: false,
                reSelectCbb: false,
                //---------------------------------------------------------

                changeFocus: false,
                // object: employee model để truyền và nhana dữ liệu từ các input
                employeeModel: {},

                // các biến liên quan đến validate
                isEmployeeCodeValidate: false,
                isFullNameValidate: false,
                isUnitIdValidate: false,
                isEmailValidate: false,
                // title cho các input validate
                inputEmployeeCodeTitle: '',
                inputFullNameTitle: '',
                inputUnitIdTitle: '',
                inputEmailTitle: ''
            }
        },
        methods: {
            // các hàm sử sụng cho combobox
            comboboxOnSelect(cbbValue) {
                this.employeeModel.UnitId = cbbValue;
                this.isShowDataCombobox = true;
                this.reSelectCbb = !this.reSelectCbb;
            },
            //mở list dropdown
            showDataCombobox() {
                this.isShowDataCombobox = true;
            },
            /**
             * Sự kiện khi blur combobox
             * Author HieuNV
             * */
            comboboxOnBlur() {
                this.isShowDataCombobox = false;
                if (this.employeeModel.UnitId == '' || this.employeeModel.UnitId == undefined) {
                    this.isUnitValidate = true;
                    this.inputUnitTitle = ResourceVn.ValidateMessenger.requiedField('UnitId');
                } else {
                    this.isUnitIdValidate = false;
                    this.inputUnitIdTitle = '';
                }
            },
            //---------------------------------------------------
            /**
             * Ẩn dialog
             * Author HieuNV
             * */
            hideDialog() {
                this.$emit('hideDialog');
            },
            /**
             * Sự kiện khi click vào btn X
             * Author HieuNV
             * */
            btnXOnClick() {
                if (JSON.stringify(this.originalModel) === JSON.stringify(this.employeeModel)) {
                    this.$emit('hideDialog');
                } else {
                    PopupEventBus.$emit('showPopup', 'save', ResourceVn.PopupMessenger.changedData(), 'question', 'xyz');
                }
            },
            /**
             * Sự kiện khi click btn Cất
             * */
            btnSaveOnClick() {
                this.save('save');
            },
            /**
             * Sự kiện khi click btn Cất và thêm
             * */
            btnSaveAndAddOnClick() {
                this.save('saveAndAdd');
            },
            /**
             * Bắt đầu thực hiện check validate trước khi hiện dialog save confirm
             * @param modeForPopup
             * Author HieuNV
             */
            save(modeForPopup) {
                var listNull = [];
                for (let i = 0; i < this.requiedList.length; i++) {
                    var fieldName = this.requiedList[i];
                    if (this.employeeModel[fieldName] == "" || this.employeeModel[fieldName] == undefined) {
                        listNull.push(fieldName);
                        this[`is${fieldName}Validate`] = true;
                        this[`input${fieldName}Title`] = ResourceVn.ValidateMessenger.requiedField(fieldName);
                    }
                }
                if (listNull.length > 0) {
                    PopupEventBus.$emit('showPopup', 'messenger', this[`input${listNull[0]}Title`], 'danger', 't');
                } else {
                    if (this.mode == 'add') {
                        PopupEventBus.$emit('showPopup', modeForPopup, ResourceVn.PopupMessenger.saveConfirm(), 'question', 'xyz');
                    } else {
                        PopupEventBus.$emit('showPopup', modeForPopup, ResourceVn.PopupMessenger.updateConfirm(), 'question', 'xyz');
                    }                   
                }
            },
            /**
             * formatDate
             * @param _date
             * Author HieuNV
             */
            formatDateToValue(_date) {
                return FormatData.formatDateToValue(_date);
            },
            /**
             * THực hiện validate các trường bắt buộc nhập
             * @param fieldName
             * Author HieuNV
             */
            validateRequied(fieldName) {
                if (this.employeeModel[`${fieldName}`] == "" || this.employeeModel[`${fieldName}`] == undefined) {
                    this[`is${fieldName}Validate`] = true;
                    this[`input${fieldName}Title`] = ResourceVn.ValidateMessenger.requiedField(fieldName);
                } else {
                    this[`is${fieldName}Validate`] = false;
                    this[`input${fieldName}Title`] = '';
                }
            },
            /**
             * Validate Email
             * Author HieuNV
             * */
            validateEmail() {
                if (!Validation.isValidEmail(this.employeeModel.Email) && this.employeeModel.Email !== '') {
                    this.isEmailValidate = true;
                    this.inputEmailTitle = ResourceVn.ValidateMessenger.invalidField('Email');
                } else {
                    this.isEmailValidate = false;
                    this.inputEmailTitle = '';
                }
            },
            /**
             * Gọi employee api để thực hiện post dữ liệu, nhận res hoặc error để xử lí
             * Author HieuNV
             * */
            add() {
                return new Promise((resolve) => {
                    console.log(this.employeeModel);
                    EmployeeApi.add(this.employeeModel).then(() => {
                        ToastMessengerEventBus.$emit('showMes', "Thêm mới thành công!", 'success');
                        EventBus.$emit('loadTableData');
                        resolve(true);
                    }).catch((error) => {
                        PopupEventBus.$emit('showPopup', 'messenger', error.response.data.userMsg, 'warning', 't');
                        if (error.response.data.userMsg.includes('Mã nhân viên')) {
                            this.isEmployeeCodeValidate = true;
                            this.inputEmployeeCodeTitle = error.response.data.userMsg;
                        } else if (error.response.data.userMsg.includes('Họ và tên')) {
                            this.isFullNameValidate = true;
                            this.inputFullNameTitle = error.response.data.userMsg;
                        } else if (error.response.data.userMsg.includes('Đơn vị')) {
                            this.isUnitIdValidate = true;
                            this.inputUnitIdTitle = error.response.data.userMsg;
                        }
                        resolve(false);
                    })
                });
            },
            /**
             * Gọi employee api để thực hiện put dữ liệu, nhận res hoặc error để xử lí
             * Author HieuNV
             * */
            update() {
                return new Promise((resolve) => {
                    EmployeeApi.update(this.myEmployeeId, this.employeeModel).then(() => {
                        ToastMessengerEventBus.$emit('showMes', "cập nhật thành công!", 'success');
                        EventBus.$emit('loadTableData');
                        resolve(true);
                    }).catch(error => {
                        PopupEventBus.$emit('showPopup', 'messenger', error.response.data.userMsg, 'warning', 't');
                        if (error.response.data.userMsg.includes('Mã nhân viên')) {
                            this.isEmployeeCodeValidate = true;
                            this.inputEmployeeCodeTitle = error.response.data.userMsg;
                        } else if (error.response.data.userMsg.includes('Họ và tên')) {
                            this.isFullNameValidate = true;
                            this.inputFullNameTitle = error.response.data.userMsg;
                        } else if (error.response.data.userMsg.includes('Đơn vị')) {
                            this.isUnitIdValidate = true;
                            this.inputUnitIdTitle = error.response.data.userMsg;
                        }
                        resolve(false);
                    })
                });
            }
        },
        watch: {
            // khi reOpen thay đổi => form được mở lại, sử dụng mode để khởi tạo các biến liên quan
            isReOpen() {
                // auto focus vào input EmployeeCode
                this.changeFocus = !this.changeFocus;

                if (this.mode == 'add') {
                    // kiểm tra nếu là mode clone thì lấy thông tin gán vào
                    if (this.isCloneMode) {
                        EmployeeApi.getById(this.myEmployeeId).then(res => {
                            this.employeeModel = res.data;
                            this.reSelectCbb = !this.reSelectCbb;
                            this.employeeModel.Gender = 1;
                            EmployeeApi.getNewCode().then((res) => {
                                this.employeeModel.EmployeeCode = res.data;
                                this.originalModel = Object.assign({}, this.employeeModel);
                            })
                        }).catch(error => {
                            console.log(error.response);
                        })
                    } else {
                        EmployeeApi.getNewCode().then((res) => {
                            this.employeeModel.Gender = 1;
                            this.employeeModel.EmployeeCode = res.data;
                            this.originalModel = Object.assign({}, this.employeeModel);
                        })
                    }
                } else if (this.mode == 'update') {
                    EmployeeApi.getById(this.myEmployeeId).then(res => {
                        this.employeeModel = res.data;
                        this.reSelectCbb = !this.reSelectCbb;
                        this.originalModel = Object.assign({}, this.employeeModel);
                    })
                }
            }
        },
        created() {
            this.employeeModel = Object.assign({}, EmployeeModel);

            // lấy dữ liệu đơn vị unit
            UnitApi.getAll().then(res => {
                this.comboboxList = res.data;
            })

            // Lắng nghe sự kiên confirm cất
            EventBus.$on('confirmToSave', async () => {
                if (this.mode == 'add') {
                    let success = await this.add();
                    if (success) {
                        this.$emit('hideDialog');
                    } else {
                        console.log('nônno')
                    }
                } else if (this.mode == 'update') {
                    let success = await this.update();
                    if (success) {
                        this.$emit('hideDialog');
                    } else {
                        console.log('nônno')
                    }
                }
            })

            // Lắng nghe sự kiện confirm cất và thêm
            EventBus.$on('confirmToSaveAndAdd', async () => {
                if (this.mode == 'add') {
                    let success = await this.add();
                    if (success) {
                        this.employeeModel = Object.assign({}, EmployeeModel);
                        EmployeeApi.getNewCode().then((res) => {
                            this.employeeModel.EmployeeCode = res.data;
                            this.originalModel = Object.assign({}, this.employeeModel);
                        })
                        this.changeFocus = !this.changeFocus;
                    } else {
                        console.log('nônno')
                    }
                } else if (this.mode == 'update') {
                    let success = await this.update();
                    if (success) {
                        this.employeeModel = Object.assign({}, EmployeeModel);
                        EmployeeApi.getNewCode().then((res) => {
                            this.employeeModel.EmployeeCode = res.data;
                            this.originalModel = Object.assign({}, this.employeeModel);
                        })
                        this.changeFocus = !this.changeFocus;
                    } else {
                        console.log('nônno')
                    }
                }
            })

        }
    }
</script>


<style scoped>

    .dialog {
    }

        .dialog .dialog-modal {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #000;
            opacity: 0.4;
            z-index: 17999;
        }

        .dialog .dialog-content {
            z-index: 18000;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 900px;
            height: 600px;
            background-color: #fff;
            border-radius: 3px;
            overflow: hidden;
        }

    .dialog-header {
        width: 100%;
        height: 73px;
        position: relative;
        display: flex;
        justify-content: flex-start;
        align-items: center;
        padding-left: 32px;
        background-color: #fff;
        box-sizing: border-box;
    }

    .dialog-title {
        font-size: 24px;
        font-weight: 700;
        line-height: 73px;
        margin-right: 20px;
    }

    .choose-user {
        margin-left: 10px;
        margin-right: 40px;
    }


    .x-btn {
        position: absolute;
        top: 12px;
        right: 12px;
        cursor: pointer;
    }

    .help-btn {
        position: absolute;
        top: 12px;
        right: 41px;
        cursor: pointer;
    }

    .dialog-body {
        margin-left: 32px;
        width: calc( 100% - 64px );
        height: calc(100% - 148px);
        box-sizing: border-box;
    }

    .dialog-up {
        height: 220px;
        width: 100%;
        float: left;
    }

    .dialog-down {
        width: 100%;
        height: calc(100% - 250px);
        float: left;
    }

    .dialog-row {
        width: 100%;
        height: 65px;
        display: flex;
        justify-content: space-between;
    }

    .dialog-row-lite {
        width: 624px;
        height: 65px;
        display: flex;
        justify-content: space-between;
    }

    .hatf-row-dialog {
        width: 405px;
        height: 100%;
        display: flex;
        justify-content: space-between;
    }

    .prop-item {
        height: 100%;
        width: auto;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
    }

        .prop-item p {
            padding: 0;
            margin: 0;
            font-size: 12px;
            font-weight: 700;
            padding-bottom: 5px;
        }

    .dialog-footer {
        width: calc(100% - 64px);
        margin-left: 32px;
        height: 75px;
        box-sizing: border-box;
        display: flex;
        justify-content: space-between;
        align-items: center;
        border-top: 1px solid #E0E0E0;
    }

    .btns-wrapper {
        height: auto;
        width: auto;
        display: flex;
    }

        .btns-wrapper button:first-child {
            margin-right: 10px;
        }
</style>