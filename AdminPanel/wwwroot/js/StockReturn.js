let modal;
const API = AppConfig.apiBaseUrl + "/api/StockReturn";

$(document).ready(function () {

    // 🔥 Modal init (IMPORTANT ID match)
    modal = new bootstrap.Modal(document.getElementById('stockReturnModal'));

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Stock Return");
        modal.show();
    });

    $("#btnSave").click(saveData);

    // Auto balance qty
    $("#quantity").on("input", function () {
        $("#balQty").val($(this).val());
    });
});


// ================= LOAD =================
function loadData() {

    showLoader();

    $.get(API, function (res) {

        let data = res?.data || res;

        let html = "";

        if (!Array.isArray(data)) {
            console.error("Invalid response:", res);
            return;
        }

        data.forEach((x, i) => {

            html += `<tr>
                <td>${i + 1}</td>
                <td>${x.fromOffice || '-'}</td>
                <td>${x.toOffice || '-'}</td>
                <td>${x.shipperName || '-'}</td>
                <td>${formatDate(x.returnDate)}</td>
                <td>${x.srItemName || '-'}</td>
                <td>${x.quantity || 0}</td>
                <td>${x.startNo || 0}</td>
                 <td>${x.endNo || 0}</td>
                  <td>${x.balQuantity || 0}</td>

                <td class="text-end">
                    <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.srid})">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.srid})">
                        <i class="bi bi-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        $("#tblData").html(html);

    }).always(hideLoader);
}


// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = parseInt($("#srid").val()) || 0;

    let user = sessionStorage.getItem("username") || "Admin";

    let obj = {
        srid: id,
        fromOffice: $("#fromOffice").val(),
        toOffice: $("#toOffice").val(),
        shipperName: $("#shipperName").val(),
        returnDate: $("#returnDate").val(),
        srItemName: $("#itemName").val(),
        quantity: parseInt($("#quantity").val()),
        startNo: $("#startNo").val(),
        endNo: $("#endNo").val(),
        balQuantity: $("#balQty").val(),
        isActive: $("#isActive").val(),
        createdBy: id === 0 ? user : null,
        modifiedBy: id > 0 ? user : null
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    Swal.fire({
        title: id ? "Update record?" : "Create record?",
        icon: "question",
        showCancelButton: true
    }).then(res => {

        if (!res.isConfirmed) return;

        showLoader();

        $.ajax({
            url: url,
            type: type,
            contentType: "application/json",
            data: JSON.stringify(obj),

            success: function () {

                Swal.fire({
                    icon: "success",
                    title: id ? "Updated Successfully" : "Created Successfully",
                    timer: 1500,
                    showConfirmButton: false
                });

                modal.hide();
                loadData();
            },

            error: function (err) {
                console.error(err);
                Swal.fire("Error", "Something went wrong", "error");
            },

            complete: hideLoader
        });
    });
}


// ================= EDIT =================
function edit(id) {

    showLoader();

    $.get(API + "/" + id, function (res) {

        let x = res?.data || res;

        console.log("EDIT DATA:", x);

        $("#srid").val(x.srid);
        $("#fromOffice").val(x.fromOffice);
        $("#toOffice").val(x.toOffice);
        $("#shipperName").val(x.shipperName);

        $("#returnDate").val(formatDateInput(x.returnDate));

        $("#itemName").val(x.srItemName);
        $("#quantity").val(x.quantity);
        $("#startNo").val(x.startNo);
        $("#endNo").val(x.endNo);
        $("#balQty").val(x.balQuantity);
        $("#isActive").val(mapIsActive(x.isActive));
        $("#modalTitle").text("Edit Stock Return");

        modal.show();

    }).always(hideLoader);
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (!res.isConfirmed) return;

        showLoader();

        $.ajax({
            url: API + "/" + id,
            type: "DELETE",

            success: function () {
                Swal.fire("Deleted!", "", "success");
                loadData();
            },

            complete: hideLoader
        });
    });
}


// ================= VALIDATION =================
function validate() {

    if (!$("#fromOffice").val())
        return Swal.fire("Validation", "From Office required", "warning"), false;

    if (!$("#toOffice").val())
        return Swal.fire("Validation", "To Office required", "warning"), false;

    if (!$("#returnDate").val())
        return Swal.fire("Validation", "Date required", "warning"), false;

    if (!$("#itemName").val())
        return Swal.fire("Validation", "Item required", "warning"), false;

    if (!$("#quantity").val())
        return Swal.fire("Validation", "Quantity required", "warning"), false;

    return true;
}


function mapIsActive(val) {
    val = (val || "").toString().trim().toLowerCase();
    return (val === "yes" || val === "true" || val === "1" || val === "y") ? "Yes" : "No";
}
// ================= HELPERS =================
function clearForm() {
    $("#stockReturnModal input").val('');
}

function formatDate(date) {
    return date ? new Date(date).toLocaleDateString("en-GB") : '';
}

// 🔥 NO -1 DATE ISSUE
function formatDateInput(date) {
    return date ? date.split('T')[0] : '';
}

function showLoader() {
    $("body").append('<div id="loader" class="spinner-border text-primary" style="position:fixed;top:50%;left:50%;z-index:9999;"></div>');
}

function hideLoader() {
    $("#loader").remove();
}