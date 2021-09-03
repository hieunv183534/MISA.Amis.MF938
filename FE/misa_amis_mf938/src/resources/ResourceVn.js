export default class ResourceVn {
    // fieldName tiếng việt
    static DisplayName = {
        EmployeeCode: 'Mã nhân viên',
        FullName: 'Tên',
        Email: 'Email',
        UnitId: 'Đơn vị'
    }

    // các messenger của popup
    static PopupMessenger = {
        saveConfirm: () => {
            return "Bạn có muốn thêm bản ghi vào database không?";
        },
        deleteConfirm: (id) => {
            return `Bạn có thực sự muốn xóa Nhân viên <${id}> không`
        },
        updateConfirm: () => {
            return "Bạn có muốn cập nhật bản ghi không?";
        },
        changedData: () => {
            return "Dữ liệu đã bị thay đổi, bạn có muốn cất không?";
        },
    }


    // các messenger validate
    static ValidateMessenger = {
        requiedField: (fieldName) => {
            return `${this.DisplayName[`${fieldName}`]} không được để trống`;
        },
        invalidField: (fieldName) => {
            return `${this.DisplayName[`${fieldName}`]} không đúng định dạng`;
        }
    }
}