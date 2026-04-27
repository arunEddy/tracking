let modal;
const API = AppConfig.apiBaseUrl + "/api/AirlineMaster";
function showLoader() { $("#loader").show(); }
function hideLoader() { $("#loader").hide(); }

$(document).ready(function () {

    modal = new bootstrap.Modal(document.getElementById('airlineModal'));

    loadData();

    $("#btnAdd").click(function () {
        clearForm();
        $("#modalTitle").text("Add Airline");
        modal.show();
    });

    $("#btnSave").click(function () {
        saveData();
    });

    $("#btnSearch").click(function () {
        loadData($("#search").val());
    });
});

function validateForm() {

    let isValid = true;

    // reset
    $(".is-invalid").removeClass("is-invalid");

    if (!$("#airlineCode").val().trim()) {
        $("#airlineCode").addClass("is-invalid");
        isValid = false;
    }

    if (!$("#airlineName").val().trim()) {
        $("#airlineName").addClass("is-invalid");
        isValid = false;
    }

    if (!$("#slab1").val()) {
        $("#slab1").addClass("is-invalid");
        isValid = false;
    }

    if (!isValid) {
        Swal.fire({
            icon: "warning",
            title: "Validation Error",
            text: "Please fill all required fields (*)"
        });
    }

    return isValid;
}
// LOAD
function loadData(search = "") {

    $.ajax({
        url: API + "?search=" + search,
        type: "GET",
        success: function (res) {
            console.log(res);

            let data = res?.data || [];
            if (!Array.isArray(data)) {
                if (data.data) data = data.data;
                else if (data.result) data = data.result;
                else data = [data];
            }
            bindTable(res);
        }
        ,
        error: function (err) {
            console.log("ERROR:", err.responseText); 
        }
    });
}


function bindTable(data) {

    let html = "";

    
    if (!Array.isArray(data)) {

       
        if (data.data) data = data.data;
        else if (data.result) data = data.result;
        else data = [data];
    }

    data.forEach(x => {

        html += `<tr>
            <td>${x.airid}</td>
            <td>${x.airlineCode}</td>
            <td>${x.airlineName}</td>
            <td>${x.slab1}</td>
            <td>${x.slab2}</td>
            <td>${x.slab3}</td>
            <td>${x.slab4}</td>
            <td>${x.slab5}</td>
            <td class="text-end">
                <button class="btn btn-sm btn-outline-warning me-1" onclick="edit(${x.airid})">
                    <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-outline-danger" onclick="deleteRec(${x.airid})">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        </tr>`;
    });

    $("#tblData").html(html);
}

// SAVE
function saveData() {

    if (!validateForm()) return; 

    let obj = {
        airid: parseInt($("#airlineId").val()) || 0,

        airlineCode: $("#airlineCode").val(),
        airlineName: $("#airlineName").val(),

        slab1: parseInt($("#slab1").val()) || 0,
        slab2: parseInt($("#slab2").val()) || 0,
        slab3: parseInt($("#slab3").val()) || 0,
        slab4: parseInt($("#slab4").val()) || 0,
        slab5: parseInt($("#slab5").val()) || 0
    };

    let id = obj.airid;

    if (!id || id == "0") {

        // CREATE
        $.ajax({
            url: API,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj),

            success: function () {

                Swal.fire({
                    icon: "success",
                    title: "Success",
                    text: "Airline created successfully",
                    timer: 1500,
                    showConfirmButton: false
                });

                modal.hide();
                loadData();
            },

            error: function (err) {
                Swal.fire("Error", err.responseText, "error");
            }
        });

    } else {

        // UPDATE
        $.ajax({
            url: API + "/" + id,
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(obj),

            success: function () {

                Swal.fire({
                    icon: "success",
                    title: "Updated",
                    text: "Airline updated successfully",
                    timer: 1500,
                    showConfirmButton: false
                });

                modal.hide();
                loadData();
            },

            error: function (err) {
                Swal.fire("Error", err.responseText, "error");
            }
        });
    }
}


// EDIT
function edit(id) {

    $.get(API + "/" + id, function (x) {
        $("#airlineId").val(x.data.airid);
        $("#airlineCode").val(x.data.airlineCode);
        $("#airlineName").val(x.data.airlineName);
        $("#slab1").val(x.data.slab1);
        $("#slab2").val(x.data.slab2);
        $("#slab3").val(x.data.slab3);
        $("#slab4").val(x.data.slab4);
        $("#slab5").val(x.data.slab5);

        $("#modalTitle").text("Edit Airline");
        modal.show();
    });
}


// DELETE
function deleteRec(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#6c757d",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                url: API + "/" + id,
                type: "DELETE",

                success: function () {

                    Swal.fire({
                        icon: "success",
                        title: "Deleted",
                        text: "Record deleted successfully",
                        timer: 1500,
                        showConfirmButton: false
                    });

                    loadData();
                }
            });

        }
    });
}


// CLEAR
function clearForm() {

    $("#airlineId").val('');
    $("#airlineCode").val('').removeClass("is-invalid");
    $("#airlineName").val('').removeClass("is-invalid");

    $("#slab1").val('').removeClass("is-invalid");
    $("#slab2").val('');
    $("#slab3").val('');
    $("#slab4").val('');
    $("#slab5").val('');
}