%extend cipher__Address {
	char getVersion(){
		return $self->Version;
    }
    	void setVersion(char pValue){
		$self->Version = pValue;
    }
}

%extend GoSlice {
	int isEqual(GoSlice *slice){
		return (($self->len == slice->len)) && (memcmp($self->data,slice->data, sizeof(GoSlice_))==0 );
	}

	void convertString(_GoString_ data){
		$self->data = data.p;
		$self->len = strlen(data.p);
		$self->cap = $self->len;
	}

	void setAtChar(char p,unsigned long long i){
		((char *) $self->data)[i] = p;
	}

	void getString(_GoString_ *out){
		out->p = (char *)$self->data;
		out->n = strlen((char *)$self->data);
}

int  getAtString(int index, _GoString_ *out){
	int i;
	GoString *iStr ;
	memset(iStr, 0, sizeof(GoString));
for (i = 0, iStr = (GoString*) $self->data; i <= index; ++i, ++iStr) {
	if(i == index){
		memset(&out, 0, sizeof(_GoString_));
		memcpy(&out,&iStr,sizeof(_GoString_));
		return 0;
		}
}
return 1;
}

}

%extend _GoString_ {
	int SetString(char * str){
		$self->p = str;
		$self->n = strlen(str);
	}

	char * getString(){
		return (const char *)$self->p;
	}
}

%extend cipher_SHA256 {
    	_GoString_ getStr(){
		_GoString_ str;
		str.p = (const char*)$self->data;
		str.n = strlen(str.p);
		return str;
    }
}

%extend coin__Transaction {
    int	setInnerHash(cipher_SHA256 h){
		memset($self->InnerHash, 0, sizeof(cipher__SHA256));
			cipher_SHA256_assignFrom($self->InnerHash,&h);
			return 0;
    }

		cipher_SHA256 GetInnerHash(){
		cipher_SHA256 h;
memset(&h, 0, sizeof(cipher__SHA256));
			cipher_SHA256_assignFrom(&h,&$self->InnerHash);
			return h;
	}
}
