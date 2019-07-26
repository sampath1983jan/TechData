ChunkedUploader = function (file, options) {
    if (!this instanceof ChunkedUploader) {
        return new ChunkedUploader(file, options);
    }
    $.extend(this, options);
    this.file = file;
    this.upload_token = getUniqueId();
    //this.options = $.extend({
    //    url:this
    //}, options);

    this.Index = 0;
    this.param = options.param;
    this.url = options.url;
    this.file_size = this.file.size;
    this.chunk_size = (1024 * options.chuckSize); // 100KB
    this.range_start = 0;
    this.range_end = this.chunk_size;
    this.success = options.success;
    this.error = options.error;
    this.progress = options.progress;
    this.id = options.id;
    this.plc = options.plc;
    if ('mozSlice' in this.file) {
        this.slice_method = 'mozSlice';
    }
    else if ('webkitSlice' in this.file) {
        this.slice_method = 'webkitSlice';
    }
    else {
        this.slice_method = 'slice';
    }
};

ChunkedUploader.prototype = {

    // Internal Methods __________________________________________________

    _upload: function () {
        var self = this,
            chunk;

        // Slight timeout needed here (File read / AJAX readystate conflict?)
        //  setTimeout(function () {
        // Prevent range overflow
        if (self.range_end > self.file_size) {
            self.range_end = self.file_size;
        }
        chunk = self.file[self.slice_method](self.range_start, self.range_end);
        var formData = new FormData();
        formData.append('ChunkNumber', self.Index);
        formData.append('ChunkSize', self.chunk_size);
        formData.append('totalSize', self.file_size);
        formData.append("unique", self.upload_token);
        formData.append('FileName', self.file.name);
        formData.append("uploadingProcess", self.param);
        formData.append('file', chunk);
        $.ajax({
            url: self.url,
            type: 'post',
            data: formData,
            contentType: false,
            processData: false,
            mimeType: 'application/octet-stream',
            mydata: self,
            //headers: (self.range_start !== 0) ? {
            //    'Content-Range': ('bytes ' + self.range_start + '-' + self.range_end + '/' + self.file_size)
            //} : {},
            success: self._onChunkComplete,
            error: self._onError
        });
        // }, 20);
    },
    _onError: function (s, v) {
        var othis = this.mydata;
        this.mydata.isError = true;
        console.log(s + " : " + v);
        othis.error(s, v);
    },
    // Event Handlers ____________________________________________________
    _onChunkComplete: function (fp) {
        // If the end range is already the same size as our file, we
        // can assume that our last chunk has been processed and exit
        // out of the function.
        var othis = this.mydata;
        if (othis.range_end === othis.file_size) {
            var v = {};
            v.FileName = othis.file.name;
			v.uniqueID = othis.upload_token;
			v.FullPath = fp;
            v.extension = othis.file.name.substring(othis.file.name.lastIndexOf('.') + 1);
            othis.success(v);
            othis.isError = false;
            return;
        }

        // Update our ranges
        othis.range_start = othis.range_end;
        othis.range_end = othis.range_start + othis.chunk_size;

        // Continue as long as we aren't paused
        if (!othis.is_paused) {
            othis.Index = othis.Index + 1;
            othis._upload();
        }
        othis.progress(othis.Index, othis.chunk_size, othis.file_size);
    },
    // Public Methods ____________________________________________________
    start: function () {
        this._upload();
    },
    pause: function () {
        this.is_paused = true;
    },
    resume: function () {
        this.is_paused = false;
        this._upload();
    }
};


getUniqueId = function () {
    var dateObject = new Date();
    var uniqueId =
        dateObject.getFullYear() + '' +
        dateObject.getMonth() + '' +
        dateObject.getDate() + '' +
        dateObject.getTime() + "" + Math.floor(Math.random() * (100000000000000 - 1 + 1)) + 1;
    return uniqueId;
};