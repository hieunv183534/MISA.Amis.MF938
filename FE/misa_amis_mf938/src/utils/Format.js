export default class FormatData{

    /**
     * định dang ngày về dạjng dd/mm/yyyy
     * @param {*} _date 
     * @returns 
     */
    static formatDate(_date) {
        if (_date != null) {
          var date = new Date(_date);
          var day = date.getDate();
          day = day < 10 ? "0" + day : day;
          var month = date.getMonth() + 1;
          month = month < 10 ? "0" + month : month;
          var year = date.getFullYear();
          return day + "/" + month + "/" + year;
        } else {
          return "";
        }
    }

    /**
     * format date về dạng yyyy-mm-dd
     * @param _date
     */
    static formatDateToValue(_date) {
        if (_date != null) {
            var date = new Date(_date);
            var day = date.getDate();
            day = day < 10 ? "0" + day : day;
            var month = date.getMonth() + 1;
            month = month < 10 ? "0" + month : month;
            var year = date.getFullYear();
            return year + "-" + month + "-" + day;
        } else {
            return "";
        }
    }

      /**
       * định dạng tiền
       * @param {*} _salary 
       * @returns 
       */
      static formatMoney(_salary) {
        if (_salary != null) {
          /*var salary = _salary.toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");*/
          return _salary.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
          /*return salary;*/
        } else {
          return "";
        }
      }

      /**
       * Lấy giới tính text vn heo giá trị int
       */
      static formatGender(_gender){
        switch(_gender){
          case 0:
            return "Nữ";
          case 1: 
            return "Nam";
          case 2: 
            return "Khác";
          default: 
            return null;
        }
      }
}