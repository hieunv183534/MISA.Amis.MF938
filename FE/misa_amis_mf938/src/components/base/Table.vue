<template>
    <div class="grid">
        <table>
            <colgroup>
                <col style="min-width:36px;" />
                <col v-for="column in tableColumns"
                     :key="column.fieldName"
                     :style="column.width" />
                <col style="min-width:120px;" />
            </colgroup>
            <thead style="z-index: 10000;">
            <th style="position: sticky; left: 0; z-index: 10000; background-color: #ECEEF1; ">
                <p style="height: 34px; padding-top: 4px; padding-left: 7px; padding-right: 7px; box-sizing: border-box; ">
                    <Checkbox/>
                </p>
            </th>
            <th v-for="column in tableColumns"
                :key="column.fieldName"
                :style="column.style">
                <p>
                    {{ column.displayName }}
                </p>
            </th>
            <th style="position: sticky; right: 0; z-index: 10000; background-color: #ECEEF1; ">
                <p style="border-right: 0 ;border-left: 1px solid #c7c7c7; line-height: 34px;">CHỨC NĂNG</p>
            </th>
            </thead>
            <tbody>
                <tr v-for="(item, index) in tableDataList" :key="index" @dblclick="trOnDbClick(item.EmployeeId)">
                    <td style="position: sticky; left: 0; z-index: 900; background-color: #fff; ">
                        <p style="line-height: 44px; padding-top: 12px; padding-left: 7px; padding-right: 7px; box-sizing: border-box;">
                            <Checkbox @checkboxOnClick="checkCheck(item.EmployeeId)"/>
                        </p>
                    </td>
                    <td v-for="column in tableColumns"
                        :key="column.fieldName"
                        :style="column.style"
                        :title="item[column.fieldName]">
                        <p v-if="column.formatType == 'money'">
                            {{ formatSalary(item[column.fieldName]) }}
                        </p>
                        <p v-else-if="column.formatType == 'ddmmyyyy'">
                            {{ formatDate(item[column.fieldName]) }}
                        </p>
                        <p v-else-if="column.formatType == 'gender'">
                            {{ formatGender(item[column.fieldName]) }}
                        </p>
                        <p v-else> {{ item[column.fieldName] }} </p>
                    </td>
                    <td style="position: sticky; right: 0; z-index: 900; background-color: #fff; ">
                        <p style="border-right: 0 ;border-left: 1px dotted #c7c7c7; line-height: 44px;">
                            <FunctionTool @showFunctionList="showFunctionList" 
                                          @hideFunctionList="hideFunctionList"
                                          :toolId="item.EmployeeId"
                                          :toolCode="item.EmployeeCode"
                                          @btnEditOnClick="btnEditOnClick"/>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
        <FunctionToolList/>
    </div>
</template>

<script>
    import FormatData from "../../utils/Format";
    import FunctionTool from "./FunctionTool"
    import FunctionToolList from "./FunctionToolList"
    import Checkbox from "./Checkbox"

    export default {
        name: "Table",
        components: {
            FunctionTool, Checkbox, FunctionToolList
        },
        props: {
            tableColumns: Array,
            tableDataList: Array,
        },
        methods: {
            /**
             * formatDate
             * @param _date
             */
            formatDate(_date) {
                return FormatData.formatDate(_date);
            },
            /**
             * formatMoney
             * @param _salary
             */
            formatSalary(_salary) {
                return FormatData.formatMoney(_salary);
            },
            /**
             * Lấy giá trị text của genger
             * @param _gender
             */
            formatGender(_gender) {
                return FormatData.formatGender(_gender);
            },
            /**
             * Hiển thị functionList với position truyền vào
             * @param str
             * @param employeeId
             * Author HieuNV
             */
            showFunctionList(str,employeeId) {
                this.isShowFunctionList = true;
                this.functionListPositionStr = str;
                this.functionToolListId = employeeId;
            },
            /**
             * Ẩn functionList
             * Author HieuNv
             * */
            hideFunctionList() {
                this.isShowFunctionList = false;
            },
            /**
             * Sự kiện khi dbClick vào 1 hàng=> hiện dialog sửa
             * @param employeeId
             */
            trOnDbClick(employeeId) {
                console.log(employeeId);
                this.$emit('chooseAnEmployee', employeeId);
            },
            /**
             * Sự kiện khi click btn sửa trên các dòng
             * @param employeeId
             * Author HieuNV
             */
            btnEditOnClick(employeeId) {
                this.$emit('chooseAnEmployee', employeeId);
            },
            /**
             * Sự kiên khi click checkbox trên các hàng
             * @param id
             */
            checkCheck(id) {
                if (this.checkedEmployees.includes(id)) {
                    this.checkedEmployees = this.checkedEmployees.filter((e) => e !== id);
                } else {
                    this.checkedEmployees.push(id);
                }
                this.$emit('input', this.checkedEmployees);
            }
        },
        data() {
            return {
                checkedEmployees:[]
            }
        }
    };
</script>

<style >
    .grid {
        width: 100%;
        height: auto;
        max-height: calc(100% - 46px);
        overflow: scroll;
        background-color: #fff;
        box-sizing: border-box;
        float: left;
        border-left: 20px solid #fff;
        border-right: 20px solid #fff;
    }

    table {
        border-collapse: collapse;
        width: auto;
        height: auto;
        display: table;
    }

    thead {
        width: auto;
        height: auto;
        position: sticky;
        top: 0;
        background-color: #fff;
        box-sizing: border-box;
    }

    tbody {
        height: auto;
        width: 100%;
    }

    td,
    th {
        overflow: hidden !important;
        white-space: nowrap !important;
        text-overflow: ellipsis !important;
    }

    th {
        font-size: 13px;
        position: sticky;
        top: 0;
        z-index: 300;
        background-color: #ECEEF1;
        height: 34px;
        box-shadow: inset 0 -1px 0 #c7c7c7;
        padding-bottom: 1px 0;
    }

        th p {
            display: block;
            margin: 0;
            border-right: 1px solid #c7c7c7;
            padding-left: 10px;
            padding-right: 10px;
            line-height: 34px;
        }

    td {
        height: 44px;
        box-sizing: border-box;
    }

        td p {
            height: 44px;
            display: block;
            margin: 0;
            border-right: 1px dotted #c7c7c7;
            padding-left: 10px;
            padding-right: 10px;
            line-height: 44px;
            overflow: hidden !important;
            white-space: nowrap !important;
            text-overflow: ellipsis !important;
        }

    tbody tr {
        border-bottom: 1px solid #c7c7c7;
    }

        tbody tr:hover {
            background-color: #F3F8F8 !important;
        }

            tbody tr:hover td {
                background-color: #F3F8F8 !important;
            }
</style>