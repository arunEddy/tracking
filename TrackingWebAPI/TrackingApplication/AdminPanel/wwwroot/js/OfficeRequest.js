//const apiUrl = AppConfig.apiBaseUrl + "/api/OfficeRequest;

let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('officeModal'));

    loadData();

    $("#btnAdd").click(function () {
        clearForm();
        $("#modalTitle").text("Add Office Request");
        modal.show();
    });

    $("#btnSave").click(function () {
        saveData();
    });
});


// ================= LOAD =================
function loadData() {
    $.ajax({
        url: AppConfig.apiBaseUrl + "/api/_OfficeRequestMaster",
        type: "GET",
        success: function (res) {
            bindTable(res);
        },
        error: function () {
            alert("Error loading data");
        }
    });
}
function bindTable(res) {

    console.log("API RESPONSE:", res); // 🔥 debug

    let data = Array.isArray(res) ? res : res.data;

    let html = "";

    if (!data || data.length === 0) {
        $("#tblData").html("<tr><td colspan='26'>No Data Found</td></tr>");
        return;
    }

    data.forEach(x => {

        html += `<tr>
            <td>${x.ormid}</td>
            <td>${x.referenceBy}</td>
            <td>${x.referenceName}</td>
            <td>${x.officeName}</td>
            <td>${x.planAdopted}</td>
            <td>${x.amountPaid === "Yes" ? 'Yes' : 'No'}</td>
            <td>${x.depositAmount}</td>
            <td>${x.name}</td>
            <td>${x.address}</td>
            <td>${x.mobile}</td>
            <td>${x.phone}</td>
            <td>${x.pincode}</td>
            <td>${x.city}</td>
            <td>${x.state}</td>
            <td>${x.personalNo}</td>
            <td>${x.eMail}</td>
            <td>${x.panNumber}</td>
            <td>${x.serviceTaxNumber}</td>
            <td>${x.document1}</td>
            <td>${x.document2}</td>
            <td>${x.document3}</td>
            <td>${x.document4}</td>
            <td>${x.document5}</td>
            <td>${x.document6}</td>
            <td>${x.status}</td>
            <td>
                <button class="btn btn-warning btn-sm" onclick="edit(${x.ormid})">Edit</button>
                <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.ormid})">Delete</button>
            </td>
        </tr>`;
    });

    $("#tblData").html(html);
}

// ================= BIND TABLE =================
//function bindTable(data) {

//    let html = "";

//    data.forEach(x => {

//        html += `<tr>
//            <td>${x.ormid}</td>
//            <td>${x.referenceBy}</td>
//            <td>${x.referenceName}</td>
//            <td>${x.officeName}</td>
//            <td>${x.planAdopted}</td>
//           <td>${x.amountPaid === "Yes" ? 'Yes' : 'No'}</td>
//            <td>${x.depositAmount}</td>
//            <td>${x.name}</td>
//            <td>${x.address}</td>
//            <td>${x.mobile}</td>
//            <td>${x.phone}</td>
//            <td>${x.pincode}</td>
//            <td>${x.city}</td>
//            <td>${x.state}</td>
//            <td>${x.personalNo}</td>
//            <td>${x.eMail}</td>
//            <td>${x.panNumber}</td>
//            <td>${x.serviceTaxNumber}</td>
//            <td>${x.document1}</td>
//            <td>${x.document2}</td>
//            <td>${x.document3}</td>
//            <td>${x.document4}</td>
//            <td>${x.document5}</td>
//            <td>${x.document6}</td>
//            <td>${x.status}</td>
//            <td>
//                <button class="btn btn-sm btn-warning" onclick="edit(${x.ORMID})">Edit</button>
//                <button class="btn btn-sm btn-danger" onclick="deleteRec(${x.ORMID})">Delete</button>
//            </td>
//        </tr>`;
//    });

//    $("#tblData").html(html);
//}


// ================= SAVE =================

function validateForm() {

    let isValid = true;

    $(".text-danger").text("");

    // ✅ Safe value getter
    function getValue(id) {
        let el = $(id);
        return el.length ? (el.val() || "").toString().trim() : "";
    }

    // ✅ Common required checker
    function checkField(id, errorId, message) {
        let value = getValue(id);

        if (!value || value === "-1") {
            $(errorId).text(message);
            isValid = false;
        }
    }

    // 🔹 Required fields
    checkField("#ReferenceBy", "#ReferenceByError", "Reference By is required");
    checkField("#ReferenceName", "#ReferenceNameError", "Reference Name is required");
    checkField("#OfficeName", "#OfficeNameError", "Office Name is required");
    checkField("#Plan", "#PlanError", "Please select a plan");
    checkField("#ContactName", "#ContactNameError", "Name is required");
    checkField("#Mobile", "#MobileError", "Mobile is required");
    checkField("#Address", "#AddressError", "Address is required");
    checkField("#Pincode", "#PincodeError", "Pincode is required");
    checkField("#City", "#CityError", "City is required");
    checkField("#State", "#StateError", "State is required");

    // 🔹 Mobile validation
    let mobile = getValue("#Mobile");
    if (mobile && !/^[0-9]{10}$/.test(mobile)) {
        $("#MobileError").text("Enter valid 10 digit mobile");
        isValid = false;
    }

    // 🔹 Email validation
    let email = getValue("#Email");
    if (email && !/^\S+@\S+\.\S+$/.test(email)) {
        $("#EmailError").text("Enter valid email");
        isValid = false;
    }

    // 🔹 Pincode validation
    let pincode = getValue("#Pincode");
    if (pincode && !/^[0-9]{6}$/.test(pincode)) {
        $("#PincodeError").text("Enter valid 6 digit pincode");
        isValid = false;
    }

    return isValid;
}


function saveData() {
    if (!validateForm()) {
        return;
    }
    let obj = {
        ReferenceBy: $("#ReferenceBy").val(),
        ReferenceName: $("#ReferenceName").val(),
        OfficeName: $("#OfficeName").val(),
        PlanAdopted: $("#Plan").val(),

        AmountPaid: $("#AmountPaid").is(":checked") ? "Yes" : "No",

        DepositAmount: $("#DepositAmount").val(),

        Name: $("#ContactName").val(),
        Address: $("#Address").val(),
        Mobile: $("#Mobile").val(),
        Phone: $("#Phone").val(),
        EMail: $("#Email").val(),
        Pincode: $("#Pincode").val(),
        City: $("#City").val(),
        State: $("#State").val(),
        PersonalNo: $("#PersonalNo").val(),
        PANNumber: $("#PAN").val(),
        serviceTaxNumber: $("#ServiceTax").val(),
        document1: $("#Document1").val(),
        document2: $("#Document2").val(),
        document3: $("#Document3").val(),
        document4: $("#Document4").val(),
        document5: $("#Document5").val(),
        document6: $("#Document6").val(),
        createdBy: $("#createdBy").val(),
        status: $("#Status").val()
    };

    if (!obj.ORMID) {
        // CREATE
        $.ajax({
            url: AppConfig.apiBaseUrl + "/api/_OfficeRequestMaster/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function () {
                Swal.fire({
                    icon: "success",
                    title: "Saved!",
                    text: "Record saved successfully 🎉",
                    timer: 1500,
                    showConfirmButton: false
                });

                modal.hide();
                loadData();
            }
        });
    } else {
        // UPDATE
        $.ajax({
            url: AppConfig.apiBaseUrl + "/api/_OfficeRequestMaster/" + obj.ORMID,
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function () {
                Swal.fire({
                    icon: "success",
                    title: "Saved!",
                    text: "Record Updated successfully 🎉",
                    timer: 1500,
                    showConfirmButton: false
                });

                modal.hide();
                loadData();
            }
        });
    }
}


// ================= EDIT =================
function edit(id) {

    $.ajax({
        url: AppConfig.apiBaseUrl + "/api/_OfficeRequestMaster/" + id,
        type: "GET",
        success: function (x) {

            $("#ORMID").val(x.ormid);

            $("#ReferenceBy").val(x.referenceBy);
            $("#ReferenceName").val(x.referenceName);
            $("#OfficeName").val(x.officeName);
            $("#Plan").val(x.planAdopted);

            // ✅ FIX: string → checkbox
            $("#AmountPaid").prop("checked", x.amountPaid === "Yes");

            $("#DepositAmount").val(x.depositAmount);

            $("#ContactName").val(x.name);
            $("#Mobile").val(x.mobile);
            $("#Phone").val(x.phone);
            $("#PersonalNo").val(x.personalNo);
            $("#Email").val(x.eMail);

            $("#Address").val(x.address);
            $("#Pincode").val(x.pincode);
            $("#City").val(x.city);
            $("#State").val(x.state);

            $("#PAN").val(x.panNumber);
            $("#ServiceTax").val(x.serviceTaxNumber);

            $("#modalTitle").text("Edit Office Request");
            modal.show();
        }
    });
}


// ================= DELETE =================
function deleteRec(id) {

    if (confirm("Are you sure?")) {

        $.ajax({
            url: AppConfig.apiBaseUrl + "/api/_OfficeRequestMaster/" + id,
            type: "DELETE",
            success: function () {

                Swal.fire({
                    icon: "success",
                    title: "Deleted!",
                    text: "Record has been deleted.",
                    timer: 1500,
                    showConfirmButton: false
                });

                loadData();
            },
            error: function () {
                Swal.fire("Error", "Delete failed", "error");
            }
        });
    }
}


// ================= CLEAR =================
function clearForm() {
    $("input, textarea").val('');
    $("select").val('');
    $("#AmountPaid").prop("checked", false);
    $("#Id").val('');
}



$(document).ready(function () {
    bindDepositAmount();
    // Dropdown change event
    $("#Plan").change(function () {
        bindDepositAmount();
    });

});


function bindDepositAmount() {

    let planAmountMap = {
        "SILVER": 5000.00,
        "GOLD": 10000.00
    };

    let selectedPlan = $("#Plan").val();

    let amount = planAmountMap[selectedPlan] || "";

    $("#DepositAmount").val(amount);
}