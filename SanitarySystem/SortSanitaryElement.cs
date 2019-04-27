public static Element[] (Document document, ICollector<ElementId> collector){
  // kolejność 
  // tee, elbow, transaction, rest fitting
  // accessory
  //mechanical 
  var elements = (from id in collector
                 select document.GetElement(id)).toArray();
  int lenght = elements.Count();
  bool Change = true;
  while(Change == true){
    for(int i; i< lenght; i++ ){
      if(elements[i] is FamilyInstance){
        FamilyInstance fI = elements[i] as FamilyInstance;
        
      }
      else
      {
        
      }
    }
  }

}
