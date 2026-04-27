const API = AppConfig.apiBaseUrl + "/api/DeliveryBoyMasters";
let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal("#deliveryModal");

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Delivery Boy");
        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnSearch").click(() => loadData($("#search").val()));

    $("#appAccess").change(toggleSim);
});


// ================= LOAD =================
function loadData(search = "") {

    $("#tblData").html("<tr><td colspan='10'>Loading...</td></tr>");

    $.get(API + "?search=" + search, function (res) {

        let data = res.data || res;
        let html = "";

        if (!data.length) {
            html = "<tr><td colspan='10'>No Data</td></tr>";
        } else {

            data.forEach(x => {

                html += `<tr>
                    <td>${x.dbmid}</td>
                    <td>${x.officeName}</td>
                    <td>${x.boyCode}</td>
                    <td>${x.boyName}</td>
                    <td>${x.mobileNo}</td>
                    <td>${x.employeeName}</td>
                     <td>${x.isActive === "Y" ? 'Yes' : 'No'}</td>
                    <td>${x.allowMobileApplication === "Y" ? 'Yes' : 'No'}</td>
                    <td>${x.simNo || '-'}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="edit(${x.dbmid})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.dbmid})">Delete</button>
                    </td>
                </tr>`;
            });
        }

        $("#tblData").html(html);
    });
}


// ================= VALIDATION =================
function validate() {

    if (!$("#name").val()) return err("Office Name required");
    if (!$("#code").val()) return err("Boy Code required");
    if (!$("#boyName").val()) return err("Boy Name required");


    return true;
}

function err(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}


// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#id").val()) || 0;

    let obj = {
        dbmid: id,
        officeName: $("#name").val(),
        boyCode: $("#code").val(),
        boyName: $("#boyName").val(),
        mobileNo: $("#mobile").val(),
        employeeName: $("#empName").val(),
        simNo: $("#simno").val(),
        allowMobileApplication: $("#appAccess").is(":checked") ? "Y" : "N",
        isActive: $("#active").is(":checked") ? "Y" : "N",
        createdBy: "Admin",
        modifyedBy:"Admin",
        endDate: ""
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {
            Swal.fire("Success", "Saved Successfully", "success");
            modal.hide();
            loadData();
        },
        error: err => showError(err)
    });
}


// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res.data || res;

        $("#id").val(x.dbmid);
        $("#name").val(x.officeName);
        $("#code").val(x.boyCode);
        $("#boyName").val(x.boyName);
        $("#mobile").val(x.mobileNo);
        $("#empName").val(x.employeeName);
        $("#appAccess").prop("checked", x.allowMobileApplication === "Y");
        toggleSim();

        $("#simno").val(x.simNo);
        $("#active").prop("checked", x.isActive === "Y");

        modal.show();
    });
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(r => {

        if (r.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: function () {
                    Swal.fire("Deleted", "", "success");
                    loadData();
                }
            });
        }
    });
}


// ================= TOGGLE SIM =================
function toggleSim() {

    if ($("#appAccess").is(":checked")) {
        $("#simno").prop("disabled", false);
    } else {
        $("#simno").prop("disabled", true).val("");
    }
}


// ================= CLEAR =================
function clearForm() {
    $("#id").val(0);
    $("input").val('');
    $("#active").prop("checked", true);
    $("#simno").prop("disabled", true);
}


// ================= ERROR =================
function showError(err) {

    let msg = "Error";

    if (err.responseJSON && err.responseJSON.errors) {
        msg = Object.values(err.responseJSON.errors).flat().join("\n");
    }

    Swal.fire("Error", msg, "error");
}