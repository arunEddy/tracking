const API = AppConfig.apiBaseUrl + "/api/CustomerWiseReBookDeliveryAddress";
let modal;

$(document).ready(function () {

    const modalEl = document.getElementById('mainModal');

    if (!modalEl) {
        console.error("Modal #mainModal not found ❌");
        return;
    }

    modal = new bootstrap.Modal(modalEl);

    loadData();

    $("#btnAdd").click(() => {
        clearForm();
        $("#modalTitle").text("Add Record");

        document.activeElement.blur(); 

        modal.show();
    });

    $("#btnSave").click(saveData);
    $("#btnSearch").click(() => loadData($("#search").val()));
});


// ================= LOAD =================
function loadData(search = "") {

    $("#tblData").html("<tr><td colspan='8'>Loading...</td></tr>");

    $.get(API + "?search=" + search, function (res) {

        let data = res.data || res;
        let html = "";

        if (!data.length) {
            html = "<tr><td colspan='8'>No Data Found</td></tr>";
        } else {
            data.forEach(x => {

                html += `<tr>
                    <td>${x.cwbid}</td>
                    <td>${x.shipperName}</td>
                    <td>${x.reBookConsigneeName}</td>
                    <td>${x.deliveryAddress}</td>
                    <td>${x.pincode}</td>
                    <td>${x.city}</td>
                    <td>${x.state}</td>
                    <td>
                        <button class="btn btn-warning btn-sm" onclick="edit(${x.cwbid})">Edit</button>
                        <button class="btn btn-danger btn-sm" onclick="deleteRec(${x.cwbid})">Delete</button>
                    </td>
                </tr>`;
            });
        }

        $("#tblData").html(html);
    });
}


// ================= VALIDATION =================
function validate() {

    let errors = [];

    function check(id, msg) {
        if (!$(id).val().trim()) errors.push(msg);
    }

    check("#shipperName", "Shipper Name required");
    check("#reBookConsigneeName", "Consignee Name required");
    check("#deliveryAddress", "Address required");
    check("#pincode", "Pincode required");
    check("#city", "City required");
    check("#state", "State required");

    if (errors.length > 0) {
        Swal.fire("Validation", errors.join("<br>"), "warning");
        return false;
    }

    return true;
}


// ================= SAVE =================
function saveData() {

    if (!validate()) return;

    let id = $("#id").val() || 0;

    let obj = {
        cwbid: id,
        shipperName: $("#shipperName").val(),
        reBookConsigneeName: $("#reBookConsigneeName").val(),
        deliveryAddress: $("#deliveryAddress").val(),
        pincode: $("#pincode").val(),
        city: $("#city").val(),
        state: $("#state").val(),
        isActive: $("Y").val(),
        createdBy: $("Admin").val(),
    };

    let type = id > 0 ? "PUT" : "POST";
    let url = id > 0 ? API + "/" + id : API;

    $.ajax({
        url: url,
        type: type,
        contentType: "application/json",
        data: JSON.stringify(obj),

        success: function () {

            Swal.fire({
                icon: "success",
                title: "Saved!",
                timer: 1200,
                showConfirmButton: false
            });

            modal.hide();
            loadData();
        },
        error: function () {
            Swal.fire("Error", "Something went wrong", "error");
        }
    });
}


// ================= EDIT =================
function edit(id) {

    $.get(API + "/" + id, function (res) {

        let x = res.data || res;
       
        $("#id").val(x.cwbid);
        $("#shipperName").val(x.shipperName);
        $("#reBookConsigneeName").val(x.reBookConsigneeName);
        $("#deliveryAddress").val(x.deliveryAddress);
        $("#pincode").val(x.pincode);
        $("#city").val(x.city);
        $("#state").val(x.state);

        $("#modalTitle").text("Edit Record");

        document.activeElement.blur();

        modal.show();
    });
}


// ================= DELETE =================
function deleteRec(id) {

    Swal.fire({
        title: "Delete this record?",
        icon: "warning",
        showCancelButton: true
    }).then(res => {

        if (res.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",
                success: function () {

                    Swal.fire({
                        icon: "success",
                        title: "Deleted",
                        timer: 1200,
                        showConfirmButton: false
                    });

                    loadData();
                }
            });
        }
    });
}


// ================= CLEAR =================
function clearForm() {
    $("#id").val(0);
    $("input, textarea").val('');
}