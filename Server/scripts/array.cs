//_______________________________________________________________________________________________________________________________
// DescX Notes:
//		Index-based arrays for safer/easier to read code.
//		Requires New() and Delete().
//
$Array::DefaultName = "Array::Index";		// The "type name" prefixed to instance indicies
$Array::DefaultNull = "";					// The default null value when an array is invalid

// "API":
//
//	Array::New					Register a new array. Specify what NULL should be, and/or a custom name. Return the name.
//	Array::Delete				Delete a registered array.
//	Array::Get					Index accessor
//	Array::Insert				Range-enabled insert
//	Array::Fill					Insert a delimited list of values starting at a given position
//	Array::EraseRange			Null a range of elements. Do nothing else.
//	Array::Shrink				Remove all NULL values and compact the array
//	Array::RemoveRange			EraseRange, and then Shrink
//	Array::Valid				Check if the array was registered with New()
//	Array::Copy					Copy a registered array into destination registered array. Frees the destination first.
//	Array::CopyToWorld			Quick hack for saving arrays in TRPG. Dumps array data from $Array:: space to $world::Array:: space
//	Array::CopyFromWorld		Same as CopyToWorld, but in reverse.
//
// Underlying globals:
//		$Array::Total				The TOTAL number of calls to New() that generated a new array.
//
//		$Array::Size[name]			The number of elements currently in this array.
//		$Array::Data[name]			The name of this array.
//		$Array::Data[name, X]		The data inside this array
//		$Array::NullValue[name]		The value considered to be NULL for this array's elements. Use with Shrink() to deduplicate/etc
//_______________________________________________________________________________________________________________________________
//
// EXAMPLE 		USAGE 		AND 		EXECUTABLE 		TEST
//
//					!!! STOP !!!
//
//	THIS 		TEST 		DELETES 	ALL 		ARRAYS
//
function Array::TestFeaturesAndEcho() {
	
	// test setup	
	for(%x=0;%x<$Array::Total;%x++)
		Array::Delete($Array::DefaultName @ %x);
	$Array::Total = 0;
	$Array::Freed = "";	
	function Array::TestPrint(%array) { 
		for(%x=0;%x<$Array::Size[%array];%x++) 
			echo("    " @ %array @ "[" @ %x @ "] = " @ Array::Get(%array,%x)); 
	}
	
	echo("--------------------------------");
	echo("- Array test");
	echo("--------------------------------");
	echo("Make 5 new arrays. Work with index 4");
		%array = Array::New();
		%array = Array::New();
		%array = Array::New();
		%array = Array::New();
		%array = Array::New();
	
	echo("--------------------------------");
	echo("Ranged and random insertion");
		Array::Insert(%array, 1, 3);
		Array::Insert(%array, 2, 5, 10);
		Array::Insert(%array, 3, 7);	
		Array::TestPrint(%array);
	echo("PASS IF    " @ $Array::DefaultName @ "4" @ "[] = _ _ _ 1 _ 2 2 3 2 2 2");
	
	
	echo("--------------------------------");
	echo("Shrink");
		Array::Shrink(%array);
		Array::TestPrint(%array);
	echo("PASS IF    " @ $Array::DefaultName @ "4" @ "[] = 1 2 2 3 2 2 2");
	
	echo("--------------------------------");
	echo("Erase elements 3 & 4, no shrink");
		Array::EraseRange(%array, 3, 4);
		Array::TestPrint(%array);
	echo("PASS IF    " @ $Array::DefaultName @ "4" @ "[] = 1 2 2 _ _ 2 2");
	
	echo("--------------------------------");
	echo("Copy to invalid unmanaged name? FAIL");
		Array::Copy(%array, "myCustomName");
		Array::TestPrint("myCustomName");
	echo("PASS IF    nothing prints");
	
	echo("--------------------------------");
	echo("Copy managed to NEW unmanaged");
		%unmanagedArray = Array::New("", "myCustomName");
		Array::Copy(%array, "myCustomName");
		Array::TestPrint("myCustomName");
	echo("PASS IF    myCustomName" @                 "[] = 1 2 2 _ _ 2 2");
	
	echo("--------------------------------");
	echo("Copy unmanaged to managed name");
		Array::Copy(%unmanagedArray, $Array::DefaultName @ "2");
		Array::TestPrint($Array::DefaultName @ "2");
	echo("PASS IF    " @ $Array::DefaultName @ "2" @ "[] = 1 2 2 _ _ 2 2");
	
	echo("--------------------------------");
	echo("Delete managed index 4, then 3");
		Array::Delete($Array::DefaultName @ "4");
		Array::Delete($Array::DefaultName @ "3");
		Array::TestPrint($Array::DefaultName @ "4");
		Array::TestPrint($Array::DefaultName @ "3");
	echo("PASS IF    nothing prints");
	
	echo("--------------------------------");
	echo("Oldest Delete was 4 - does new array reuse 4 ???");
		%testStringA = "This had numbers.";
		%newArray = Array::New();
		Array::Insert(%newArray, %testStringA);
		Array::TestPrint(%newArray);
	echo("PASS IF    " @ $Array::DefaultName @ "4" @ "[] = \"" @ %testStringA @ "\"");
	
	echo("--------------------------------");
	echo("Delete managed and myCustomName");
		Array::Delete("myCustomName");
		for(%x=0;%x<$Array::Total;%x++)
			Array::Delete($Array::DefaultName @ %x);
		for(%x=0;%x<$Array::Total;%x++)
			Array::TestPrint($Array::DefaultName @ %x);
		Array::TestPrint("myCustomName");
		echo($Array::Freed);
	echo("PASS IF    3 0 1 2 4");
	echo("--------------------------------");
}


//_______________________________________________________________________________________________________________________________
// Retrieve the value of an array. This code uses global variables to work around the inability to pass arrays to functions
function Array::Get(%array, %index) {
	if(!Array::Valid(%array))
		return $Array::DefaultNull;
	%whatIsNull	= $Array::NullValue[%array];
	%pos 		= floor(%index);
	if(Math::isInteger(%pos) && %pos < $Array::Size[%array])
		return $Array::Data[%array, %index];
	else return %whatIsNull;
}

//_______________________________________________________________________________________________________________________________
// Insert a specific value into an array at the end
function Array::Push(%array, %value) {
	return Array::Insert(%array, %value, -1);
}

//_______________________________________________________________________________________________________________________________
// Insert a specific value into [a range of] indicies. If the start position is -1, assume end-of-list insertion. Blank assumes front
function Array::Insert(%array, %value, %indexStart, %indexEnd) {
	%inserted = 0;
	if(Array::Valid(%array)) {
		%whatIsNull	= $Array::NullValue[%array];	
		if(%indexStart < 0)		%pos = $Array::Size[%array];
		else 					%pos = floor(%indexStart);	
		
		if(%indexEnd == "")		%end = %pos;
		else					%end = floor(%indexEnd);
		
		if(Math::isInteger(%pos) && %pos >= 0 && Math::isInteger(%end)) {		
			for(%x=0;%x<=%end;%x++) {
				if(%x >= $Array::Size[%array]) {
					$Array::Data[%array,%x] = %whatIsNull;
					$Array::Size[%array] = %x + 1;
				}
				if(%x >= %pos) {
					%inserted++;
					$Array::Data[%array,%x] = %value;
				}
			}
		}
	}
	return %inserted;
}

//_______________________________________________________________________________________________________________________________
// Fill the array with a delimited string of values starting at indexStart
function Array::Fill(%array, %flatValueList, %delimiter, %indexStart) {
	%inserted = 0;
	if(Array::Valid(%array)) {
		%whatIsNull	= $Array::NullValue[%array];	
		if(%indexStart < 0)	%pos = $Array::Size[%array];
		else 				%pos = floor(%indexStart);
	
		if(Math::isInteger(%pos) && %pos >= 0 && Math::isInteger(%end)) {			
			%value = "";
			%nextValuePos = String::findSubStr(%flatValueList,%delimiter);
			if(%nextValuePos != -1) {
				%value 			= String::newGetSubStr(%flatValueList,0,%nextValuePos);
				%flatValueList 	= String::newGetSubStr(%flatValueList,%nextValuePos+1,9999);
			} else if (%flatValueList != $Array::NullValue[%array]) {
				%value 			= %flatValueList;
				%flatValueList 	= $Array::NullValue[%array];
			}
		
			for(%x=0; %value != ""; %x++) {
				if(%x >= $Array::Size[%array]) {
					$Array::Size[%array] = %x + 1;
				}
				if(%x >= %pos){					
					%inserted++;
					$Array::Data[%array,%x] = %value;
					%nextValuePos = String::findSubStr(%flatValueList,%delimiter);
					if(%nextValuePos != -1) {
						%value 			= String::newGetSubStr(%flatValueList,0,%nextValuePos);
						%flatValueList 	= String::newGetSubStr(%flatValueList,%nextValuePos+1,9999);
					} else if (%flatValueList != $Array::NullValue[%array]) {
						%value 			= %flatValueList;
						%flatValueList 	= $Array::NullValue[%array];
					} else break;
				}				
			}
		}
	}
	return %inserted;
}

//_______________________________________________________________________________________________________________________________
// Erase [a range of] indicies from an array without changing the size
function Array::EraseRange(%array, %indexStart, %indexEnd) {
	%removed=0;
	if(Array::Valid(%array)) {
		%pos = floor(%indexStart);
		if(%indexEnd == "")	%end = %pos;
		else				%end = floor(%indexEnd);
		%whatIsNull	= $Array::NullValue[%array];
		if(Math::isInteger(%pos) && %pos >= 0 && Math::isInteger(%end)) {			
			for(%x=%end;%x>=%pos;%x--){
				$Array::Data[%array,%x] = %whatIsNull;
				%removed++;
			}
		}
	}
	return %removed;
}

//_______________________________________________________________________________________________________________________________
// Remove nulls from the array and shrink it to the smallest size possible
function Array::Shrink(%array) {
	%removed 	= 0;
	if(Array::Valid(%array)) {		
		%whatIsNull	= $Array::NullValue[%array];
		%y			= 0;
		for(%x=0;%x<$Array::Size[%array];%x++) {
			if($Array::Data[%array,%x] == %whatIsNull) {
				%removed++;
			} else {
				$Array::Data[%array,%y] = $Array::Data[%array,%x];
				%y++;
			}
		}
		%oldSize = $Array::Size[%array];
		$Array::Size[%array] -= %removed;
		for(%x=$Array::Size[%array];%x<%oldSize;%x++){
			$Array::Data[%array,%x] = "";
		}		
	}
	return %removed;
}

//_______________________________________________________________________________________________________________________________
// Remove indicies from an array, and shrink the array to the minimum required size
function Array::RemoveRange(%array, %indexStart, %indexEnd) {
	%removed = Array::EraseRange(%array,%indexStart,%indexEnd);
	if(%removed > 0) Array::Shrink(%array);
	return %removed;
}

//_______________________________________________________________________________________________________________________________
// Construct an %array with an optional custom name. Pop the oldest free index off the stack if possible.
function Array::New(%whatIsNull, %optionalName) {
	if(%optionalName != "")	{
		%array = %optionalName;		
	} else {
		%topOfFreeStack = GetWord($Array::Freed,0);
		if(%topOfFreeStack != -1) {				// got something to pop? pop pop. poppitty pop paaaawwp pop.			
			$Array::Freed 	= String::replace($Array::Freed, %topOfFreeStack @ " ", "");
			%newName 		= $Array::DefaultName @ %topOfFreeStack;
		} else {
			%oldTotal = $Array::Total;
			$Array::Total++;
			if(%oldTotal >= $Array::Total) {	// In case of overflow, reuse a trap array to limit damage
				$Array::Total = %oldTotal;
				%newName = "Array::OverflowTrap";					
				dbecho(999, %newName @ ": PANIC: NO FREE ARRAYS");
			} else {
				%newName = $Array::DefaultName @ %oldTotal;
			}
		}
		%array = %newName;
	}
	
	$Array::NullValue[%array] = %whatIsNull;
	$Array::Size[%array] = 0;
	$Array::Data[%array] = %array;
	return %array;
}

//_______________________________________________________________________________________________________________________________
// Consider this array to be "valid" if it has an integer size and the datastring matches the arraystring
function Array::Valid(%array) {
	return (Math::isInteger($Array::Size[%array]) && $Array::Data[%array] == %array);
}

//_______________________________________________________________________________________________________________________________
// Copy an array into a destination array. %destination must be another array, but the user takes responsibility for trampling data
function Array::Copy(%array, %destination) {
	if(Array::Valid(%array)) {
		if(Array::Delete(%destination, true)) {	// dont free, just delete
			$Array::Data[%destination] 		= %destination;
			$Array::Size[%destination] 		= $Array::Size[%array];
			$Array::NullValue[%destination] = $Array::NullValue[%array];
			for(%x=0;%x<$Array::Size[%array];%x++)
				$Array::Data[%destination,%x] = $Array::Data[%array,%x];	
			return true;
		}
	}
	return false;
}


//_______________________________________________________________________________________________________________________________
// Copy an array TO the $world:: context
function Array::CopyToWorld(%array) {
	if(Array::Valid(%array)) {
		$world::Array::Data[%array] 			= %array;
		$world::Array::Size[%array] 			= $Array::Size[%array];
		$world::Array::NullValue[%array]		= $Array::NullValue[%array];
		for(%x=0;%x<$Array::Size[%array];%x++)
			$world::Array::Data[%array,%x] 		= $Array::Data[%array,%x];	
		return true;
	}
	return false;
}


//_______________________________________________________________________________________________________________________________
// Copy an array FROM the $world:: context
function Array::CopyFromWorld(%array) {
	if(Array::Valid(%array)) {
		$Array::Data[%array] 			= %array;
		$Array::Size[%array] 			= $world::Array::Size[%array];
		$Array::NullValue[%array]		= $world::Array::NullValue[%array];
		for(%x=0;%x<$Array::Size[%array];%x++)
			$Array::Data[%array,%x] 	= $world::Array::Data[%array,%x];	
		return true;
	}
	return false;
}

//_______________________________________________________________________________________________________________________________
// Clear its contents and counters. Assume we want to add this array to the "freed" stack (but some code may prefer GarbageCollection())
function Array::Delete(%array, %dontFree) {
	if(Array::Valid(%array)) {
		for(%x=0;%x<$Array::Size[%array];%x++)
			$Array::Data[%array,%x] = "";
		$Array::Data[%array] 		= "";
		$Array::Size[%array] 		= "";
		$Array::NullValue[%array] 	= "";
		if(%dontFree != true) {
			%index = String::replace(%array,$Array::DefaultName,"");
			if(%index != %array && String::findSubStr($Array::Freed, %index) == -1)		// dont "free" user-named objects
				$Array::Freed = $Array::Freed @ %index @ " ";
		}
		return true;
	}
	return false;
}

//_______________________________________________________________________________________________________________________________
// I forgot to include a ForEach?!?! Oh my. Here it is now.
function Array::ForEach(%array, %fn, %parms) {
	if(%parms !=""){
		for(%x=0;%x<$Array::Size[%array];%x++) {
			%call = %fn @ "(\"" @ %array @ "\"," @ %x @ ",\"" @ Array::Get(%array,%x) @ "\"," @ %parms @ ");";
			eval(%call);
		}
	} else {
		for(%x=0;%x<$Array::Size[%array];%x++) {
			%call = %fn @ "(\"" @ %array @ "\"," @ %x @ ",\"" @ Array::Get(%array,%x) @ "\");";
			eval(%call);
		}
	}
}
