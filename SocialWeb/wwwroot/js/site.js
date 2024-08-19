
function LikePost(postid) {
	$.ajax({
		url: `/User/LikePost`,
		type: 'POST',
		data: { postid: postid },
		success: function (response) {
			$(`#count-${postid}`).replaceWith(`
				<span id="count-${postid}" style="color: #B4D6CD">${response.countlike}</span>
			`);

			$(`#btnLike-${postid}`).replaceWith(`
                <a id="btnLike-${postid}" onclick="UnLikePost(${postid})" style="color: #B4D6CD">
					<i class="fa fa-thumbs-up" aria-hidden="true"></i>
				</a>
            `); // Sử dụng backticks cho chuỗi nhiều dòng
		},
	});
}

function UnLikePost(postid) {
	$.ajax({
		url: `/User/UnLikePost`,
		type: 'POST',
		data: { postid: postid },
		success: function (response) {
			$(`#count-${postid}`).replaceWith(`
				<span id="count-${postid}">${response.countlike}</span>
			`);

			$(`#btnLike-${postid}`).replaceWith(`
                <a id="btnLike-${postid}" onclick="LikePost(${postid})">
					<i class="fa fa-thumbs-up" aria-hidden="true"></i>
				</a>
            `); // Sử dụng backticks cho chuỗi nhiều dòng
		},
	});
}

function CreateComment(postid) {
	var content = $(`#contentComment-${postid}`).val();
	$.ajax({
		url: "/User/CreateComment",
		type: "POST",
		data: { postid: postid, content: content},
		success: function () {
			LoadComment(postid)
		}
	})
}
function LoadComment(postid) {
	$.ajax({
		url: "/User/LoadComments",
		type: "POST",
		data: { postid: postid },
		success: function (response) {
			$(`#commentContainer-${postid}`).html(response)
		}
	})
}
function RequestFriend(friendid) {
	$.ajax({
		url: "/Search/RequestFriend",
		type: "POST",
		data: { friendid: friendid },
		success: function () {
			$(`#btn-${friendid}`).replaceWith(`
				<button style="background-color: #FF4E88; border: 0; color: #FFDA76; width: 50px; height: 50px">
                    <i class="fa fa-clock-o" aria-hidden="true"></i>
                </button>
			`);
		}
	})
}
function AcceptRequest(friendid) {
	$.ajax({
		url: "/Home/AcceptRequest",
		type: "POST",
		data: { friendid: friendid },
		success: function () {
			LoadFriendRequest()
		}
	})
}
function DeleteRequest(friendid) {
	$.ajax({
		url: "/Home/DeleteRequest",
		type: "POST",
		data: { friendid: friendid },
		success: function () {
			LoadFriendRequest()
		}
	})
}
function LoadFriendRequest() {
	$.ajax({
		url: "/Home/LoadFriendRequest",
		type: "POST",
		data: {},
		success: function (response) {
			$(`#friendRequestContainer`).html(response)
		}
	})
};
function scrollToBottom() {
	var container = document.getElementById('messageContent');
	container.scrollTop = container.scrollHeight;
}
function LoadPrivateMessage(friendid) {
	$.ajax({
		url: "/Message/LoadPrivateMessage",
		type: "POST",
		data: { friendid: friendid },
		success: function (response) {
			$(`#messageContainer`).html(response);
			scrollToBottom();
		}
	})
}

function AddPrivateMessage(friendid) {
	var content = $(`#contentMessage-${friendid}`).val();
	
	$.ajax({
		url: "/Message/AddPrivateMessage",
		type: "POST",
		data: { friendid: friendid, content: content },
		success: function () {
			LoadPrivateMessage(friendid)
		}
	})
}
function LoadGroupMessage(groupid) {
	$.ajax({
		url: "/Message/LoadGroupMessage",
		type: "POST",
		data: { groupid: groupid },
		success: function (response) {
			$(`#messageContainer`).html(response);
			scrollToBottom();
		}
	})
}
function AddGroupMessage(groupid) {
	var content = $(`#contentMessage-${groupid}`).val();

	$.ajax({
		url: "/Message/AddGroupMessage",
		type: "POST",
		data: { groupid: groupid, content: content },
		success: function () {
			LoadGroupMessage(groupid)
		}
	})
}
