const API = AppConfig.apiBaseUrl + "/api/VendorMasters";
let modal;

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('vendorModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Vendor");
        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnSearch").click(() => loadData($("#search").val()));
});


// ================= LOAD =================
function loadData(search = "") {

    $.get(API + "?search=" + search, function (res) {

        let data = res.data || res;
        let html = "";

        data.forEach(x => {

            html += `<tr>
                <td>${x.vmid}</td>
                <td>${x.officeName || '-'}</td>
                <td>${x.vendorName || '-'}</td>
                <td>${x.vendorType || '-'}</td>
                <td>${x.name || '-'}</td>
                <td>${x.mobile || '-'}</td>
                <td>${x.eMail || '-'}</td>
                <td>${x.isActive === "true" ? 'Yes' : 'No'}</td>
                <td>
                    <button class="btn btn-sm btn-warning" onclick="edit(${x.vmid})">Edit</button>
                    <button class="btn btn-sm btn-danger" onclick="deleteRec(${x.vmid})">Delete</button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);
    });
}


// ================= VALIDATION =================
function validate() {

    if (!$("#officeName").val()) return err("Office Name required");
    if (!$("#vendorType").val()) return err("Vendor Type required");
    if (!$("#vendorCode").val()) return err("Vendor Code required");
    if (!$("#vendorName").val()) return err("Vendor Name required");

    return true;
}

function err(msg) {
    Swal.fire("Validation", msg, "warning");
    return false;
}

function getSqlDateTime() {
    let d = new Date();

    let yyyy = d.getFullYear();
    let mm = String(d.getMonth() + 1).padStart(2, '0');
    let dd = String(d.getDate()).padStart(2, '0');

    let hh = String(d.getHours()).padStart(2, '0');
    let min = String(d.getMinutes()).padStart(2, '0');
    let ss = String(d.getSeconds()).padStart(2, '0');

    return `${yyyy}-${mm}-${dd} ${hh}:${min}:${ss}`;
}
// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#vendorId").val()) || 0;

    let obj = {
        VMID: id,

        OfficeName: $("#officeName").val(),
        VendorType: $("#vendorType").val(),
        VendorCode: $("#vendorCode").val(),
        VendorName: $("#vendorName").val(),
        GSTIN: $("#gstin").val(),

        Name: $("#contactName").val(),
        Mobile: $("#mobile").val(),
        Phone: $("#phone").val(),
        EMail: $("#email").val(),

        Address: $("#address").val(),
        Pincode: $("#pincode").val(),
        City: $("#city").val(),
        State: $("#state").val(),

        IsActive: $("#active").is(":checked") ? "true" : "false",
        createdBy: "Admin",

       
    };

    console.log("Payload:", obj); // DEBUG

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
        error: function (err) {
            console.log(err.responseText);
            Swal.fire("Error", "Something went wrong", "error");
        }
    });
}


// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (x) {

        $("#vendorId").val(x.vmid);
        $("#officeName").val(x.officeName);
        $("#vendorType").val(x.vendorType);
        $("#vendorCode").val(x.vendorCode);
        $("#vendorName").val(x.vendorName);
        $("#gstin").val(x.gstin);

        $("#contactName").val(x.name);
        $("#mobile").val(x.mobile);
        $("#phone").val(x.phone);
        $("#email").val(x.eMail);

        $("#address").val(x.address);
        $("#pincode").val(x.pincode);
        $("#city").val(x.city);
        $("#state").val(x.state);

        $("#active").prop("checked", x.isActive === "true");

        modal.show();
    });
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: function () {
                    Swal.fire("Deleted!", "", "success");
                    loadData();
                }
            });
        }
    });
}


// ================= CLEAR =================
function clearForm() {
    $("input, textarea").val('');
    $("#active").prop("checked", true);
}