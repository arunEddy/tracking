const API = AppConfig.apiBaseUrl + "/api/VendorMasters";
let modal;

$(document).ready(function () {
    document.activeElement.blur();
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
    showLoader();
    $.get(API + "/" + id, function (x) {
        console.log("API se full data aaya:", x);  
        console.table(x);
        $("#vendorId").val(x.data.vmid || x.data.vmid || "");
        $("#officeName").val(x.data.officeName || "");
        $("#vendorType").val(x.data.vendorType || "");
        $("#vendorCode").val(x.data.vendorCode || "");
        $("#vendorName").val(x.data.vendorName || "");
        $("#gstin").val(x.data.gstin || "");
        $("#contactName").val(x.data.name || x.contactName || "");
        $("#mobile").val(x.data.mobile || "");
        $("#phone").val(x.data.phone || "");
        $("#email").val(x.data.eMail || x.email || "");
        $("#address").val(x.data.address || "");
        $("#pincode").val(x.data.pincode || "");
        $("#city").val(x.data.city || "");
        $("#state").val(x.data.state || "");
        const isActive = (x.data.isActive === true) || (x.data.isActive === "true") || (x.data.isActive === 1);
        $("#active").prop("checked", isActive);
       
        document.activeElement.blur();

        modal.show();


    }).fail(err => {
        Swal.fire("Error", err.responseText, "error");
    }).always(hideLoader);
}
function showLoader() {
    $("#loader").show();
}

function hideLoader() {
    $("#loader").hide();
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