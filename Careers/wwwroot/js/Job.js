function LoadJobs() {
    var searchingJob = $("#jobSearch").val();
    var industryFilter = $("#IndustryTypeId").val();
    $.ajax({
        url: JobFilterUrl,
        type: "GET",
        data: {
            jobSearch: searchingJob,
            industryType: industryFilter
        },
        success: function (data) {
            $("#joblist").html(data);
        }
    })
};
function ShowLoadingForJobs() {
    $("#joblist").toggle();
}

$("#jobSearch").on("keyup", function () {
    var term = $(this).val();
    if (term.length >= 2 || term.length == 0) {
        LoadJobs()
    }
});

$("#IndustryTypeId").on('change', function () {
    LoadJobs();
});

$(".apply").on("click", function (e) {
    $.ajax({
        type: "POST",
        url: url,
        data: {},
        success: function (data) {
            $(".apply").html("Applied");
        }
    });
    e.preventDefault();
});